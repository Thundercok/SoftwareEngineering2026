"""
Unit Tests for nesy-docai Phase 2 Extensions
"""

import pytest
from PIL import Image
from nesy_docai import (
    VisionPerceptionEngine,
    SymbolicSolverEngine,
    BoundingBoxVisualizer,
    PDFDocumentProcessor
)
from api_server import read_root, health_check


def test_visualizer():
    vision = VisionPerceptionEngine()
    solver = SymbolicSolverEngine(vision_engine=vision)
    visualizer = BoundingBoxVisualizer()

    raw_data = vision.process_invoice_image("sample.png")
    verified = solver.solve_and_verify(raw_data)

    img = Image.new("RGB", (600, 800), color="#FFFFFF")
    annotated = visualizer.annotate_invoice(img, verified)

    assert isinstance(annotated, Image.Image)
    assert annotated.size == (600, 800)


def test_pdf_processor():
    pdf_proc = PDFDocumentProcessor()
    img = pdf_proc.render_dummy_pdf_page_image(page_num=1)

    assert isinstance(img, Image.Image)
    assert img.size == (600, 800)


def test_fastapi_endpoints_direct():
    root_res = read_root()
    assert root_res["framework"] == "nesy-docai"
    assert root_res["status"] == "OPERATIONAL"

    health_res = health_check()
    assert health_res["status"] == "HEALTHY"
    assert health_res["smt_solver_ready"] is True
