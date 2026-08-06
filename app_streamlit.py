"""
Streamlit Web UI Dashboard for nesy-docai
Run with: streamlit run app_streamlit.py
"""

import json
import streamlit as st
from PIL import Image, ImageDraw
from nesy_docai import (
    VisionPerceptionEngine,
    SymbolicSolverEngine,
    TaxMasterDataVerifier,
    AuditExcelExporter
)

st.set_page_config(
    page_title="nesy-docai: Neuro-Symbolic Document AI",
    page_icon="📄",
    layout="wide"
)

st.title("📄 nesy-docai: Neuro-Symbolic Document AI Research Engine")
st.markdown("### System 1 (Visual Perception) + System 2 (Z3 SMT Constraint Solver)")

# Sidebar Configuration
st.sidebar.header("⚙️ Configuration")
model_choice = st.sidebar.selectbox("System 1 Vision Model", ["Qwen2.5-VL-3B (Local Ollama)", "PaddleOCR-VL", "Mock OCR Noise"])
enable_tax_verification = st.sidebar.checkbox("Enable Tax Master Data Verification", value=True)

# File Uploader
uploaded_file = st.file_uploader("Upload Invoice Image / PDF", type=["png", "jpg", "jpeg", "pdf"])

col1, col2 = st.columns([1, 1])

with col1:
    st.subheader("1. Input Document & Visual Bounding Boxes")
    if uploaded_file:
        image = Image.open(uploaded_file)
        st.image(image, caption="Uploaded Invoice", use_column_width=True)
    else:
        st.info("💡 Upload an invoice image or click 'Run Demo with Sample Invoice' below.")
        if st.button("Run Demo with Sample Invoice", type="primary"):
            st.session_state["run_demo"] = True

if uploaded_file or st.session_state.get("run_demo"):
    vision = VisionPerceptionEngine()
    solver = SymbolicSolverEngine(vision_engine=vision)
    tax_verifier = TaxMasterDataVerifier()
    exporter = AuditExcelExporter()

    # Step 1: System 1 Extractions
    raw_data = vision.process_invoice_image("sample_invoice.png")

    with col2:
        st.subheader("2. System 1: Raw Extractions (OCR Noise Detected)")
        st.json(raw_data)

    st.markdown("---")
    st.subheader("3. System 2: Z3 SMT Constraint Verification & Error Correction")

    # Step 2: System 2 Z3 Solver
    verified = solver.solve_and_verify(raw_data)

    if enable_tax_verification:
        tax_info = tax_verifier.verify_tax_id(verified.get("seller_tax_id", ""))
        verified["tax_verification"] = tax_info

    res_col1, res_col2 = st.columns([1, 1])

    with res_col1:
        if verified.get("audit_status") == "VERIFIED_SAT":
            st.success("✅ AUDIT STATUS: VERIFIED_SAT (100% Mathematically Correct)")
        else:
            st.error("❌ AUDIT STATUS: FLAGGED_UNSAT")

        st.json(verified)

    with res_col2:
        st.subheader("📜 Z3 Proof Certificate")
        cert = verified.get("proof_certificate", {})
        st.code(f"SMT Status: {cert.get('smt_status')}\nProof Formula: {cert.get('proof_formula')}", language="text")

        st.markdown("**Verified Constraints:**")
        for c in cert.get("constraints_verified", []):
            st.markdown(f"- `{c}`")

        # Export Button
        excel_file = exporter.export_to_excel([verified], "invoice_audit_report.xlsx")
        with open(excel_file, "rb") as f:
            st.download_button(
                label="📥 Download Excel Audit Report (.xlsx)",
                data=f,
                file_name="invoice_audit_report.xlsx",
                mime="application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
            )
