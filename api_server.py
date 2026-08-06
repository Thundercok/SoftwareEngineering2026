"""
FastAPI Enterprise REST Server for nesy-docai
Run with: uvicorn api_server:app --reload --port 8000
"""

from typing import Dict, Any, List
from fastapi import FastAPI, File, UploadFile, HTTPException
from fastapi.responses import FileResponse, JSONResponse
from nesy_docai import (
    VisionPerceptionEngine,
    SymbolicSolverEngine,
    TaxMasterDataVerifier,
    AuditExcelExporter,
    BoundingBoxVisualizer,
    PDFDocumentProcessor
)

app = FastAPI(
    title="nesy-docai API",
    description="Enterprise Neuro-Symbolic Document AI REST Server (Qwen2.5-VL System 1 + Z3 SMT System 2)",
    version="0.2.0"
)

# Initialize Core Engine Modules
vision_engine = VisionPerceptionEngine()
symbolic_solver = SymbolicSolverEngine(vision_engine=vision_engine)
tax_verifier = TaxMasterDataVerifier()
excel_exporter = AuditExcelExporter()
visualizer = BoundingBoxVisualizer()
pdf_processor = PDFDocumentProcessor()


@app.get("/")
def read_root():
    return {
        "framework": "nesy-docai",
        "version": "0.2.0",
        "system1": "Qwen2.5-VL / OCR Candidate Generator",
        "system2": "Z3 Presburger SMT Solver",
        "status": "OPERATIONAL"
    }


@app.get("/health")
def health_check():
    return {"status": "HEALTHY", "smt_solver_ready": True}


@app.post("/api/v1/parse-invoice")
async def parse_invoice(file: UploadFile = File(...)):
    """
    Parses an uploaded invoice image/PDF, executes System 1 (Vision) & System 2 (Z3 SMT Solver),
    cross-verifies tax ID, and returns the verified JSON audit record.
    """
    if not file.filename:
        raise HTTPException(status_code=400, detail="No file uploaded.")

    # 1. System 1 Perception
    raw_data = vision_engine.process_invoice_image(file.filename)

    # 2. System 2 Symbolic Solver Verification
    verified_record = symbolic_solver.solve_and_verify(raw_data)

    # 3. Tax Master Data Verification
    tax_info = tax_verifier.verify_tax_id(verified_record.get("seller_tax_id", ""))
    verified_record["tax_verification"] = tax_info

    return verified_record


@app.post("/api/v1/export-excel")
async def export_excel(audit_records: List[Dict[str, Any]]):
    """
    Exports a list of audit records to a formatted Excel workbook.
    """
    if not audit_records:
        raise HTTPException(status_code=400, detail="Audit records list cannot be empty.")

    output_path = excel_exporter.export_to_excel(audit_records, output_filepath="invoice_audit_report.xlsx")
    return FileResponse(
        path=output_path,
        filename="invoice_audit_report.xlsx",
        media_type="application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
    )


if __name__ == "__main__":
    import uvicorn
    uvicorn.run("api_server:app", host="0.0.0.0", port=8000, reload=True)
