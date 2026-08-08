"""
Streamlit Web UI Dashboard for nesy-docai (Batch Multi-File Processing Enabled)
Run with: streamlit run app_streamlit.py
"""

import os
import io
import json
import zipfile
import time
import pandas as pd
import streamlit as st
from PIL import Image
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

st.title("📄 nesy-docai: Enterprise Batch Document AI Dashboard")
st.markdown("### Neuro-Symbolic System 1 (Vision) + System 2 (Z3 SMT Presburger Solver)")

# Sidebar Configuration
st.sidebar.header("⚙️ System Configuration")
model_choice = st.sidebar.selectbox("System 1 Vision Model", ["Qwen2.5-VL-3B (Local Ollama)", "PaddleOCR-VL", "Mock OCR Noise"])
enable_tax_verification = st.sidebar.checkbox("Enable Tax Master Data Verification", value=True)
st.sidebar.markdown("---")
st.sidebar.info("💡 **Batch Processing Mode**: Select multiple PDF/Image files or upload a ZIP archive. The system processes them in a queue and merges them into a single Master Excel report.")

# Main Navigation Tabs
tab_batch, tab_single = st.tabs(["🚀 Batch Processing (Upload Hàng Loạt)", "🔍 Single Invoice Deep-Dive"])

# Initialize Engines
vision_engine = VisionPerceptionEngine()
solver_engine = SymbolicSolverEngine(vision_engine=vision_engine)
tax_verifier = TaxMasterDataVerifier()
exporter = AuditExcelExporter()

# -----------------------------------------------------------------------------
# TAB 1: BATCH PROCESSING (Upload 100+ Bills / ZIP)
# -----------------------------------------------------------------------------
with tab_batch:
    st.subheader("1. Select Multiple Files or ZIP Folder (Chọn / Kéo Thả Hàng Loạt Bill)")

    uploaded_files = st.file_uploader(
        "Upload Multiple Invoice Images, PDFs, or a ZIP Archive",
        type=["png", "jpg", "jpeg", "pdf", "zip"],
        accept_multiple_files=True,
        help="You can drag & drop dozens of invoice files or a ZIP archive containing all invoices."
    )

    col_btn1, col_btn2, col_btn3 = st.columns([1.5, 2, 2.5])
    with col_btn1:
        run_batch_btn = st.button("🚀 Audit Uploaded Files (Chạy File Đã Chọn)", type="primary", use_container_width=True)
    with col_btn2:
        batch_size = st.slider("Số bill thử nghiệm live", min_value=5, max_value=100, value=20, step=5, label_visibility="collapsed")
    with col_btn3:
        run_dynamic_batch_btn = st.button(f"⚡ Live Stress Test ({batch_size} Invoices)", type="secondary")

    if run_batch_btn or run_dynamic_batch_btn:
        records_to_process = []

        if run_dynamic_batch_btn:
            # Generate specified number of realistic mock invoices with OCR noise & Z3 verification
            companies = [
                ("0312345678", "CÔNG TY TNHH THIẾT BỊ VĂN PHÒNG SÀI GÒN"),
                ("0101234567", "CÔNG TY CP THƯƠNG MẠI DỊCH VỤ AN PHÁT"),
                ("0319876543", "CÔNG TY TNHH CÔNG NGHỆ SỐ HÀI DÓN"),
                ("0305556667", "CÔNG TY TNHH THƯƠNG MẠI MINH ĐỨC"),
                ("0109998887", "CÔNG TY CP ĐIỆN MÁY VIỆT NAM"),
                ("0400112233", "CÔNG TY TNHH NÔNG SẢN ĐÀ NẮNG"),
                ("1800445566", "CÔNG TY TNHH VẬT TƯ CẦN THƠ")
            ]
            items_pool = [
                ("Bút ký cao cấp M&G", "1O000", 10000),
                ("Tập vở HS 200 trang", "15000", 15000),
                ("Giấy in A4 Double A 70gsm", "45000", 45000),
                ("Chuột máy tính không dây Logitech", "12O000", 120000),
                ("Bàn phím cơ AKKO 3087", "1000000", 1000000),
                ("Tai nghe Bluetooth Sony", "1S00000", 1500000),
                ("Màn hình Dell UltraSharp 27 inch", "7500000", 7500000),
                ("Ổ cứng SSD Samsung 1TB", "2200000", 2200000)
            ]
            for i in range(1, batch_size + 1):
                tax_id, comp_name = companies[(i - 1) % len(companies)]
                item_desc, raw_price_str, real_price = items_pool[(i - 1) % len(items_pool)]
                qty = (i % 5) + 1
                amount = real_price * qty
                tax_val = int(amount * 0.1)
                total_val = amount + tax_val

                mock_inv = {
                    "invoice_id": f"HD-2026-{i:03d}",
                    "invoice_date": "2026-08-08",
                    "seller_tax_id": tax_id,
                    "seller_name": comp_name,
                    "line_items": [
                        {
                            "item_id": 1,
                            "description": item_desc,
                            "quantity": str(qty),
                            "unit_price": raw_price_str,  # Contains OCR noise
                            "amount": str(amount),
                            "bbox": [120, 340, 480, 360]
                        }
                    ],
                    "subtotal": str(amount),
                    "tax": f"{tax_val}OO" if i % 7 == 0 else str(tax_val),  # Occasional OCR noise in tax
                    "total": str(total_val)
                }
                records_to_process.append((f"Live_Invoice_{i:03d}.png", mock_inv))
        elif uploaded_files:
            for i, uploaded_file in enumerate(uploaded_files):
                filename = uploaded_file.name
                if filename.endswith(".zip"):
                    try:
                        zip_bytes = io.BytesIO(uploaded_file.getvalue())
                        with zipfile.ZipFile(zip_bytes) as z:
                            for zidx, zname in enumerate(z.namelist()):
                                if not zname.startswith("__MACOSX") and zname.lower().endswith(('.png', '.jpg', '.jpeg', '.pdf')):
                                    raw = vision_engine.process_invoice_image(zname)
                                    raw["invoice_id"] = f"HD-2026-{800+zidx:03d}"
                                    records_to_process.append((os.path.basename(zname), raw))
                    except Exception as e:
                        st.error(f"❌ Could not extract ZIP file `{filename}`: {str(e)}")
                else:
                    raw = vision_engine.process_invoice_image(filename)
                    raw["invoice_id"] = f"HD-2026-{890+i:03d}"
                    records_to_process.append((filename, raw))
        else:
            st.warning("⚠️ Please select files to upload or click 'Live Stress Test'!")

        if records_to_process:
            st.markdown("---")
            st.subheader(f"2. Processing Queue ({len(records_to_process)} Invoices)")

            progress_bar = st.progress(0)
            status_text = st.empty()

            processed_results = []
            table_rows = []

            for idx, (fname, raw_data) in enumerate(records_to_process):
                status_text.markdown(f"⏳ **Processing [{idx+1}/{len(records_to_process)}]:** `{fname}`...")
                
                try:
                    # Execute System 2 Z3 Solver
                    verified = solver_engine.solve_and_verify(raw_data)
                    
                    # Tax Verification
                    if enable_tax_verification:
                        tax_info = tax_verifier.verify_tax_id(verified.get("seller_tax_id", ""))
                        verified["tax_verification"] = tax_info

                    processed_results.append(verified)

                    is_sat = verified.get("audit_status") == "VERIFIED_SAT"

                    # Format row for live table
                    table_rows.append({
                        "File": fname,
                        "Invoice ID": verified.get("invoice_id", raw_data.get("invoice_id")),
                        "Date": verified.get("invoice_date", raw_data.get("invoice_date")),
                        "MST Ng Bán (Vendor Tax Code)": verified.get("seller_tax_id", raw_data.get("seller_tax_id")),
                        "Tên Ng Bán (Vendor Name)": verified.get("seller_name", raw_data.get("seller_name")),
                        "Nội Dung Diễn Giải": verified.get("description_summary", ", ".join([item.get("description", "") for item in raw_data.get("line_items", [])])),
                        "Tiền Hàng Subtotal (VND)": f"{verified.get('subtotal', 0):,}" if is_sat else f"Raw: {raw_data.get('subtotal')}",
                        "Thuế Suất Tax Rate (%)": verified.get("tax_rate", "10%") if is_sat else "N/A",
                        "Tiền Thuế VAT Tax Amount (VND)": f"{verified.get('tax', 0):,}" if is_sat else f"Raw: {raw_data.get('tax')}",
                        "Tổng Thanh Toán Total (VND)": f"{verified.get('total', 0):,}" if is_sat else f"Raw: {raw_data.get('total')}",
                        "Audit Status": "✅ VERIFIED_SAT" if is_sat else "❌ FLAGGED_UNSAT (Lỗi tính toán)",
                        "Z3 Resolution": "Z3 Auto-Repaired OCR" if is_sat else "Z3 Logic Constraint Failed"
                    })
                except Exception as ex:
                    table_rows.append({
                        "File": fname,
                        "Invoice ID": raw_data.get("invoice_id", "N/A"),
                        "Date": raw_data.get("invoice_date", "N/A"),
                        "MST Ng Bán (Vendor Tax Code)": raw_data.get("seller_tax_id", "N/A"),
                        "Tên Ng Bán (Vendor Name)": raw_data.get("seller_name", "N/A"),
                        "Nội Dung Diễn Giải": "Error processing file",
                        "Tiền Hàng Subtotal (VND)": "0",
                        "Thuế Suất Tax Rate (%)": "0%",
                        "Tiền Thuế VAT Tax Amount (VND)": "0",
                        "Tổng Thanh Toán Total (VND)": "0",
                        "Audit Status": f"❌ ERROR ({str(ex)})",
                        "Z3 Resolution": "Execution Exception"
                    })

                progress_bar.progress((idx + 1) / len(records_to_process))
                time.sleep(0.01)

            status_text.success(f"🎉 **Batch Processing Complete!** Successfully audited {len(processed_results)} invoices.")

            # Store in session state for downloading/viewing
            st.session_state["batch_results"] = processed_results
            st.session_state["table_df"] = pd.DataFrame(table_rows)

    # Display Batch Results if available
    if "table_df" in st.session_state:
        st.markdown("---")
        st.subheader("3. Live Batch Audit Summary Table (Bảng Tổng Hợp Kế Toán)")
        st.dataframe(st.session_state["table_df"], use_container_width=True)

        st.markdown("---")
        st.subheader("4. Export Consolidated Master Ledger")
        
        excel_path = exporter.export_to_excel(st.session_state["batch_results"], "master_invoice_audit_report.xlsx")
        with open(excel_path, "rb") as f:
            st.download_button(
                label="📥 Download Consolidated Master Excel Report (.xlsx)",
                data=f,
                file_name="master_invoice_audit_report.xlsx",
                mime="application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                type="primary",
                use_container_width=True
            )

# -----------------------------------------------------------------------------
# TAB 2: SINGLE INVOICE DEEP-DIVE INSPECTION
# -----------------------------------------------------------------------------
with tab_single:
    st.subheader("Single Invoice Deep-Dive & Z3 Proof Certificate Inspector")

    single_file = st.file_uploader("Upload Single Invoice for Inspection", type=["png", "jpg", "jpeg", "pdf"], key="single_uploader")

    if single_file or st.button("Inspect Sample Invoice Demo", key="btn_single_demo"):
        raw_data = vision_engine.process_invoice_image("sample_invoice.png")
        verified = solver_engine.solve_and_verify(raw_data)
        
        col_s1, col_s2 = st.columns([1, 1])
        with col_s1:
            st.markdown("#### Raw OCR Candidate Extractions (System 1)")
            st.json(raw_data)

        with col_s2:
            st.markdown("#### Z3 SMT Verified & Repaired Data (System 2)")
            st.json(verified)

        st.markdown("#### 📜 Z3 SMT Proof Certificate")
        cert = verified.get("proof_certificate", {})
        st.code(f"SMT Status: {cert.get('smt_status')}\nProof Formula: {cert.get('proof_formula')}", language="text")
        for c in cert.get("constraints_verified", []):
            st.markdown(f"- `{c}`")

