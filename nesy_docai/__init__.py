"""
nesy-docai: Neuro-Symbolic Document AI Research Engine
"""

__version__ = "0.2.0"
__author__ = "Huỳnh Nhật Huy"

from .system1_vision import VisionPerceptionEngine
from .system2_solver import SymbolicSolverEngine
from .tax_verifier import TaxMasterDataVerifier
from .exporter import AuditExcelExporter
from .visualizer import BoundingBoxVisualizer
from .pdf_processor import PDFDocumentProcessor

__all__ = [
    "VisionPerceptionEngine",
    "SymbolicSolverEngine",
    "TaxMasterDataVerifier",
    "AuditExcelExporter",
    "BoundingBoxVisualizer",
    "PDFDocumentProcessor",
]
