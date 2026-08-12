"""
macOS Native Vision Framework OCR Engine (NPU/Neural Engine Accelerated)
"""

import os
import json
import subprocess
from typing import List, Dict, Any


class MacOSVisionEngine:
    def __init__(self):
        self.swift_script_path = os.path.join(os.path.dirname(__file__), "vision_native.swift")

    def run_ocr(self, image_path: str) -> List[Dict[str, Any]]:
        """
        Invokes native Swift script running VNRecognizeTextRequest on macOS.
        Returns list of recognized text tokens with pixel bounding boxes.
        """
        if not os.path.exists(image_path) or not os.path.exists(self.swift_script_path):
            return []

        try:
            cmd = ["swift", self.swift_script_path, image_path]
            res = subprocess.run(cmd, capture_output=True, text=True, timeout=10)
            if res.returncode == 0 and res.stdout.strip():
                return json.loads(res.stdout.strip())
        except Exception:
            pass

        return []
