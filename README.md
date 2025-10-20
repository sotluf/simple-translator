# Simple Translator

**Simple Translator** is a console-based application written in **C#** that allows users to translate text between different languages using the **DeepL API**.

Features include:

- Multiple translations in a single session
- Option to continue with the same languages or choose new languages
- Error handling

## Technologies

- **Language:** C#
- **.NET Version:** 9
- **Library:** DeepL.net

## How to get an API Key?

To use this program, you need a **DeepL API key**.

**Steps:**

1. Sign up at [DeepL](https://www.deepl.com/pro-api) to get your API key.
2. Set the API key as an environment variable:

**Windows (PowerShell):**
```powershell
setx DEEPL_API_KEY "your_api_key"
````

**Linux / Mac**
```bash
export DEEPL_API_KEY="your_api_key"
````

3. Restart your terminal or IDE.
4. Run the program.

## How to use

1. Run the program in your IDE or terminal.
2. Enter source language code.
3. Enter target language code.
4. Enter the text to translate.
5. After translation, choose an option from the menu.

