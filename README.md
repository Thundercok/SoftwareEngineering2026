# nesy-docai: Neuro-Symbolic Document AI Engine

**`nesy-docai`** là hệ thống nghiên cứu & bóc tách dữ liệu hóa đơn/chứng từ tài chính dựa trên kiến trúc **Neuro-Symbolic AI (System 1 + System 2)**:

- **System 1 (Visual Perception)**: Nhận diện bố cục và bóc tách chữ từ ảnh/PDF bằng Vision-Language Model (`Qwen2.5-VL` local / OCR engine), trích xuất Bounding Box coordinates $(x_0, y_0, x_1, y_1)$.
- **System 2 (Symbolic Reasoning)**: Sử dụng **Z3 SMT Solver** kiểm chứng các quy luật đại số kế toán ($\text{LineAmount} = \text{Qty} \times \text{Price}$, $\text{Total} = \text{Subtotal} + \text{Tax}$) và tự động sửa các lỗi ký tự OCR (`O` $\rightarrow$ `0`, `l` $\rightarrow$ `1`, `S` $\rightarrow$ `5`, `B` $\rightarrow$ `8`).

---

## 📁 Cấu Trúc Thư Mục

```text
SoftwareEngineering2026/
├── nesy_docai/
│   ├── __init__.py           # Package entrypoint
│   ├── system1_vision.py     # Module OCR & Candidate Generator
│   ├── system2_solver.py     # Z3 SMT Constraint Solver Engine
│   ├── tax_verifier.py       # Đối soát Mã số thuế & Dữ liệu Tổng Cục Thuế
│   └── exporter.py           # Xuất Excel Báo cáo & Audit Trail Log
├── tests/
│   └── test_nesy_docai.py    # Automated Unit Tests
├── main.py                   # CLI Orchestrator Runner
├── requirements.txt          # Thư viện phụ thuộc
└── README.md
```

---

## 🚀 Cài Đặt & Sử Dụng

### 1. Cài đặt môi trường

```bash
pip install -r requirements.txt
```

### 2. Chạy thử nghiệm Pipeline

```bash
python main.py
```

### 3. Chạy Unit Tests

```bash
pytest tests/
```
