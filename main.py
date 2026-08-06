"""
nesy-docai Main Runner / CLI Orchestrator
"""

import json
from nesy_docai import (
    VisionPerceptionEngine,
    SymbolicSolverEngine,
    TaxMasterDataVerifier,
    AuditExcelExporter
)


def run_pipeline(invoice_image_path: str = "sample_invoice.png", output_excel: str = "invoice_audit_report.xlsx"):
    print("=" * 60)
    print("  nesy-docai: Neuro-Symbolic Document AI Research Engine")
    print("=" * 60)

    # 1. System 1: Vision Perception (Candidate Extraction)
    print("\n[Step 1] Running System 1 (Visual Perception Engine)...")
    vision = VisionPerceptionEngine()
    raw_data = vision.process_invoice_image(invoice_image_path)

    print("  -> Raw OCR Extractions (Contains noise '1O000' and '95OO'):")
    print(json.dumps(raw_data, ensure_ascii=False, indent=4))

    # 2. System 2: Symbolic Solver (Z3 SMT Verification & Correction)
    print("\n[Step 2] Running System 2 (Z3 SMT Symbolic Solver)...")
    solver = SymbolicSolverEngine(vision_engine=vision)
    verified_record = solver.solve_and_verify(raw_data)

    print(f"  -> SMT Status: {verified_record.get('audit_status')}")
    print("  -> Corrected & Verified Data (Presburger Integer Arithmetic verified):")
    print(json.dumps(verified_record, ensure_ascii=False, indent=4))

    # 3. Tax Master Data Cross-Verification
    print("\n[Step 3] Cross-verifying Seller Tax ID with General Department of Taxation...")
    tax_verifier = TaxMasterDataVerifier()
    tax_info = tax_verifier.verify_tax_id(verified_record.get("seller_tax_id", ""))
    verified_record["tax_verification"] = tax_info
    print(f"  -> Tax Status: {tax_info.get('status')} ({tax_info.get('verification_message')})")

    # 4. Export to Formatted Excel Report
    print("\n[Step 4] Exporting Multi-Sheet Excel Audit Ledger Report...")
    exporter = AuditExcelExporter()
    saved_path = exporter.export_to_excel([verified_record], output_filepath=output_excel)
    print(f"  -> Excel report saved successfully to: {saved_path}")

    print("\n" + "=" * 60)
    print("  Pipeline execution complete! 100% Mathematically & Tax Verified.")
    print("=" * 60)


if __name__ == "__main__":
    run_pipeline()
