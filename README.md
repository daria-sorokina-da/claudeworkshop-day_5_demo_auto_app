# 🐎 HorseWorld

A small full-stack demo application built for the QA automation workshop. It has a **.NET 8 Web API** backend and a **React + TypeScript** frontend. All data is **hardcoded in memory** — there is no database to set up.

## Features

| Feature | Description |
|---|---|
| 📖 **Breed Encyclopedia** | Browse 12 horse breeds with details (origin, temperament, uses, fun facts), plus an **identification quiz** that describes a mystery breed and asks you to name it. |
| ✨ **Name Generator** | Generate horse names by **style** (elegant, wild, mythical, celtic, nature) and **gender** (male, female, neutral). |
| 🐴 **Personality Quiz** | Answer 8 questions to discover *"Which horse breed are you?"* |

The breed illustrations are custom-generated SVGs served locally (see [`frontend/public/breeds/`](frontend/public/breeds/)) — no external image dependencies.

## Tech Stack

- **Backend:** ASP.NET Core 8 Web API — controllers with static in-memory data stores
- **Frontend:** Vite + React 18 + TypeScript
- **Data:** Hardcoded in C# static classes ([`src/HorseApp.Api/Data/`](src/HorseApp.Api/Data/))

## Project Structure

```
.
├── src/HorseApp.Api/          # ASP.NET Core Web API
│   ├── Controllers/           #   Breeds, Names, PersonalityQuiz
│   ├── Models/                #   Request/response record types
│   ├── Data/                  #   Hardcoded in-memory data
│   └── Program.cs             #   App setup + CORS
└── frontend/                  # Vite + React + TypeScript
    ├── src/
    │   ├── pages/             #   Encyclopedia, NameGenerator, PersonalityQuiz
    │   ├── api/               #   Typed fetch client + types
    │   └── App.tsx            #   Nav shell
    └── public/breeds/         # Generated breed SVG illustrations
```

## Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [Node.js 18+](https://nodejs.org/)

### Run the backend

```bash
dotnet run --project src/HorseApp.Api --urls "http://localhost:7070"
```

The API listens on **http://localhost:7070**.

### Run the frontend

In a second terminal:

```bash
cd frontend
npm install        # first time only
npm run dev
```

The app is served at **http://localhost:3737**. The Vite dev server proxies `/api` requests to the backend on port 7070 (configured in [`frontend/vite.config.ts`](frontend/vite.config.ts)).

## API Reference

Base URL: `http://localhost:7070`

### Breeds — `/api/breeds`

| Method | Path | Description |
|--------|------|-------------|
| `GET`  | `/api/breeds` | List all breeds (summary view) |
| `GET`  | `/api/breeds/{id}` | Full detail for one breed; `404` if not found |
| `GET`  | `/api/breeds/quiz/question` | A random identification question with 4 options |
| `POST` | `/api/breeds/quiz/answer` | Check an answer: `{ "questionId", "answerId" }` |

### Names — `/api/names`

| Method | Path | Description |
|--------|------|-------------|
| `GET`  | `/api/names/generate?style={style}&gender={gender}` | Returns 5 names. `style`: `elegant`/`wild`/`mythical`/`celtic`/`nature`; `gender`: `male`/`female`/`neutral` |
| `GET`  | `/api/names/options` | Valid style and gender values |

### Personality — `/api/personality`

| Method | Path | Description |
|--------|------|-------------|
| `GET`  | `/api/personality/questions` | All 8 questions with options |
| `POST` | `/api/personality/result` | Calculate result: `{ "answers": [{ "questionId", "optionId" }] }` |

### Example

```bash
curl "http://localhost:7070/api/names/generate?style=mythical&gender=female"
# { "names": ["Athena","Cassandra","Circe","Hecate","Andromeda"],
#   "style": "mythical", "gender": "female" }
```

## Notes

- This app exists as a target for QA automation exercises — see [`WORKSHOP_DESCRIPTION.md`](WORKSHOP_DESCRIPTION.md).
- Breed SVGs can be regenerated with `node frontend/public/breeds/generate-svgs.mjs`.

## License

[CC BY 4.0](LICENSE.md)
