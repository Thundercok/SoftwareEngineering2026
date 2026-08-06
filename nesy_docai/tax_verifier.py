"""
Tax Master Data Verifier Module
"""

from typing import Dict, Any


class TaxMasterDataVerifier:
    def __init__(self):
        # Simulated lookup database for registered Vietnamese enterprise tax IDs
        self.tax_database = {
            "0312345678": {
                "company_name": "CÔNG TY TNHH THIẾT BỊ VĂN PHÒNG SÀI GÒN",
                "status": "ACTIVE_OPERATING",
                "address": "123 Nguyễn Huệ, Phường Bến Nghé, Quận 1, TP. Hồ Chí Minh",
                "tax_authority": "Chi cục Thuế Quận 1"
            }
        }

    def verify_tax_id(self, tax_id: str) -> Dict[str, Any]:
        """
        Cross-verifies seller tax ID with General Department of Taxation database.
        """
        clean_id = str(tax_id).strip().replace("-", "")
        record = self.tax_database.get(clean_id)

        if record:
            return {
                "verified": True,
                "tax_id": clean_id,
                "company_name": record["company_name"],
                "status": record["status"],
                "address": record["address"],
                "verification_message": "Tax ID active and operating legally."
            }
        else:
            return {
                "verified": False,
                "tax_id": clean_id,
                "status": "NOT_FOUND_OR_SUSPENDED",
                "verification_message": "Tax ID not found in official registry or suspended."
            }
