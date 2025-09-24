# 🎰 Slot Machine Console App

A simple console-based slot machine game where players can place bets and try their luck.

---

## 📜 Game Rules

- Players can bet **any available balance amount**.  
- Payout multipliers:
  - **Pair (two matching numbers):** `bet * 3`
  - **Three matching numbers:** `bet * 5`
  - **Three 7s:** `bet * 10`

Good luck and have fun! 🍀

---

## ⚙️ Build Instructions

### 1. Clone or Download
Clone this repository or download it as a `.zip`.

### 2. Open in Visual Studio
- Target Framework: **.NET Framework 4.8** (or **.NET Core** / **.NET 6+** depending on your project).

### 3. Build the Solution
- Press **Ctrl + Shift + B** or go to **Build → Build Solution**.

### 4. Run the App
- Press **F5** to run with debugger or **Ctrl + F5** to run without debugging.

---

## 📦 Publish Instructions

1. In Visual Studio, open **Build → Publish \<YourProjectName\>**.  
2. Choose **Folder** (or another target like an installer) and pick an output path.  
3. Configure any publish settings (target runtime, single file, etc.) and click **Publish**.  
4. After publishing, the output folder will contain the published files. If you created an installer or `setup.exe` (via a packaging tool), that `setup.exe` will be in the chosen output.

> **Note:** Visual Studio alone won't create a Windows `setup.exe` installer unless you use a packaging tool (e.g., WiX Toolset, Inno Setup, or Visual Studio Installer Projects). If you want a simple `setup.exe`, consider using one of those tools to wrap your published files.

---

## 💻 Installation Instructions

### If an older version is installed
1. Press the **Windows** key.  
2. Search for **Add or Remove Programs**.  
3. Search for **SlotMachine** in the list.  
4. Click **Uninstall**.

### To install the new version
1. Run the generated **setup.exe** (from the Publish / installer step).  
2. Follow the installer prompts.  
3. Launch the game, place your bets, and enjoy! 🎉

---

## 🚀 Example Gameplay

