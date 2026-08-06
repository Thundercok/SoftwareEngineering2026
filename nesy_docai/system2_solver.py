"""
System 2: Symbolic Reasoning Engine (Z3 SMT Constraint Solver)
"""

from typing import Dict, Any, List
from z3 import Int, Solver, sat, Or
from .system1_vision import VisionPerceptionEngine


class SymbolicSolverEngine:
    def __init__(self, vision_engine: VisionPerceptionEngine = None):
        self.vision_engine = vision_engine or VisionPerceptionEngine()

    def solve_and_verify(self, raw_data: Dict[str, Any]) -> Dict[str, Any]:
        """
        Executes Z3 SMT constraint solving over candidate extractions.
        Returns corrected, mathematically verified invoice data and a proof log.
        """
        solver = Solver()
        line_items = raw_data.get("line_items", [])
        n = len(line_items)

        # 1. Declare Z3 Presburger Integer Variables
        z3_subtotal = Int('subtotal')
        z3_tax = Int('tax')
        z3_total = Int('total')

        z3_quantities = [Int(f'q_{i}') for i in range(n)]
        z3_prices = [Int(f'p_{i}') for i in range(n)]
        z3_amounts = [Int(f'a_{i}') for i in range(n)]

        # 2. Hard Accounting Constraints
        for i in range(n):
            # Line item Amount = Quantity * UnitPrice
            solver.add(z3_amounts[i] == z3_quantities[i] * z3_prices[i])
            solver.add(z3_quantities[i] >= 0)
            solver.add(z3_prices[i] >= 0)
            solver.add(z3_amounts[i] >= 0)

        if n > 0:
            solver.add(z3_subtotal == sum(z3_amounts))
        else:
            solver.add(z3_subtotal >= 0)

        # Total = Subtotal + Tax
        solver.add(z3_total == z3_subtotal + z3_tax)

        # Tax Rate constraint (Vietnam Tax Rates: 0%, 5%, 8%, 10% with rounding tolerance)
        tax_rates = [0, 5, 8, 10]
        tax_constraints = []
        for rate in tax_rates:
            expected_tax = (z3_subtotal * rate) / 100
            tax_constraints.append(
                (z3_tax >= expected_tax - 2) & (z3_tax <= expected_tax + 2)
            )
        solver.add(Or(tax_constraints))

        # 3. Soft Candidates Constraints from OCR candidates
        for i, item in enumerate(line_items):
            q_cands = self.vision_engine.generate_number_candidates(item.get("quantity"))
            p_cands = self.vision_engine.generate_number_candidates(item.get("unit_price"))
            a_cands = self.vision_engine.generate_number_candidates(item.get("amount"))

            if q_cands:
                solver.add(Or([z3_quantities[i] == c for c in q_cands]))
            if p_cands:
                solver.add(Or([z3_prices[i] == c for c in p_cands]))
            if a_cands:
                solver.add(Or([z3_amounts[i] == c for c in a_cands]))

        sub_cands = self.vision_engine.generate_number_candidates(raw_data.get("subtotal"))
        tax_cands = self.vision_engine.generate_number_candidates(raw_data.get("tax"))
        tot_cands = self.vision_engine.generate_number_candidates(raw_data.get("total"))

        if sub_cands:
            solver.add(Or([z3_subtotal == c for c in sub_cands]))
        if tax_cands:
            solver.add(Or([z3_tax == c for c in tax_cands]))
        if tot_cands:
            solver.add(Or([z3_total == c for c in tot_cands]))

        # 4. Check Satisfiability (SAT / UNSAT)
        if solver.check() == sat:
            model = solver.model()
            corrected_items = []
            for i in range(n):
                corrected_items.append({
                    "item_id": line_items[i].get("item_id"),
                    "description": line_items[i].get("description"),
                    "quantity": model[z3_quantities[i]].as_long(),
                    "unit_price": model[z3_prices[i]].as_long(),
                    "amount": model[z3_amounts[i]].as_long(),
                    "bbox": line_items[i].get("bbox")
                })

            subtotal_val = model[z3_subtotal].as_long()
            tax_val = model[z3_tax].as_long()
            total_val = model[z3_total].as_long()

            proof_certificate = {
                "smt_status": "SAT",
                "proof_formula": "Subtotal = Sum(LineAmounts) AND Total = Subtotal + Tax",
                "constraints_verified": [
                    f"Line items verified: {n}",
                    f"Subtotal = {subtotal_val}",
                    f"Tax = {tax_val} (Rate ~{round((tax_val/subtotal_val)*100 if subtotal_val else 0)}%)",
                    f"Total = {total_val}"
                ]
            }

            return {
                "audit_status": "VERIFIED_SAT",
                "invoice_id": raw_data.get("invoice_id"),
                "invoice_date": raw_data.get("invoice_date"),
                "seller_tax_id": raw_data.get("seller_tax_id"),
                "seller_name": raw_data.get("seller_name"),
                "line_items": corrected_items,
                "subtotal": subtotal_val,
                "tax": tax_val,
                "total": total_val,
                "bboxes": raw_data.get("bboxes"),
                "proof_certificate": proof_certificate
            }
        else:
            return {
                "audit_status": "FLAGGED_UNSAT",
                "invoice_id": raw_data.get("invoice_id"),
                "raw_data": raw_data,
                "proof_certificate": {
                    "smt_status": "UNSAT",
                    "reason": "Mathematical constraints violated or OCR noise too severe for candidate lattice."
                }
            }
