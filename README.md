# ☕ MyCoffee Ltd — Ordering System

**Course:** P532 – Software Engineering  
**Semester:** Spring 2026  
**Student:** Ishita Sutariya  
**Project:** MyCoffee Ltd Ordering System  
**Framework:** Avalonia UI (.NET 9)

---

## 📌 Project Overview

MyCoffee Ltd is a desktop-based coffee ordering system built using **Avalonia UI** and **C# (.NET)**.  
The application simulates a real-world café ordering experience where users can browse a menu, add beverages to a cart, apply tax and tip, and place an order.

This repository follows a **task-based Git workflow**, where each assignment task is developed in a separate branch and merged into `main` via Pull Requests.

---

## 📌 Task 1 — Base Ordering System

Task 1 implements the **core functionality and UI foundation** of the ordering system.

### Features Implemented
- Clean, responsive Avalonia desktop UI
- Menu with predefined beverages:
  - Espresso Shot
  - Dark Roast
  - House Blend
  - Decaf Coffee
- Add items to cart
- Remove selected item from cart
- Clear entire cart
- Automatic price calculations:
  - Subtotal
  - Tax (8%)
- Tip selection:
  - 0%, 10%, 15%, 20%
  - Custom tip input (enabled only when selected)
- Real-time total calculation
- Order confirmation dialog

---

## 📌 Task 2 Enhancement – Condiments Support

### New Additions
- Optional condiments applied to drinks:
  - Steamed Milk (+$0.30)
  - Soy (+$0.40)
  - Mocha (+$0.50)
  - Chocolate (+$0.45)
  - Whipped Milk (+$0.35)
- Condiments apply to the **next item added**
- Price updates automatically
- Description updates in cart

## 🖥️ User Interface

The UI is divided into two main sections:
- **Left:** Menu listing with “Add to Cart” actions
- **Right:** Cart summary with item list, tax, tip options, and total

Design goals:
- Clear visual hierarchy
- Professional café-style theme
- Easy interaction and feedback

---

## 🛠️ Tech Stack

- **Language:** C#
- **Framework:** .NET 9
- **UI:** Avalonia UI
- **Architecture:** Code-behind (Task 1 baseline)
- **Version Control:** Git + GitHub
- **Environment:** VS Code Dev Container (Linux)

---

## 🌱 Git Workflow

The project follows a structured branching strategy:

- `main` → Stable, merged code
- `task-1-base-code` → Task 1 implementation (merged via PR)

Future tasks will follow the same pattern:
```text
task-2-<feature>
task-3-<feature>
