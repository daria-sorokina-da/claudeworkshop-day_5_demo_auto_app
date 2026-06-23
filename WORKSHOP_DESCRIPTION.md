Let me read the exercise file first.Now I have a complete picture of the original exercise. Let me design the QA-adapted plan.

---

Here's the detailed plan for the QA automation engineers. Same structure and depth, but every example, repo, skill, hook, and scenario is anchored in their actual daily work.

---

## Plan: Hands-On Claude Code Exercise for .NET Automation QA Engineers

**Track:** QA Automation Engineers · **Duration:** ~4 hours + optional extras · **Style:** Individual or pairs

---

### Concept mapping — same skills, QA context

| Part | Concepts (identical to dev exercise) | QA-specific anchor |
|---|---|---|
| 0 — Setup | Setup check · Git safety net · ground rules | Fork the QA test repo; `dotnet test` baseline |
| 1 — Onboard & harness | Precise prompting · Plan mode · `/init` · CLAUDE.md · `@import` · Permissions · Context management · Mermaid diagram | Understand a cold test suite, diagram the test anatomy |
| 2 — Ship & debug | Agentic loop · Spec → test workflow · Atlassian Rovo MCP · Hooks · `/rewind` · Red-test debugging · Git workflow & PR | Write new tests, fix a real flaky/broken test, generate a pipeline |
| 3 — Team toolkit | Skills · Slash commands · Hooks (Post + PreToolUse) | Skills for test-data patterns, commands for test review, hooks for auto-run |
| 4 — Sub-agents | Sequential + parallel agents · Model selection & cost | Implementer agent (Testing.* services), test-writer agent, reviewer agent |
| Optional | GitHub MCP · Azure DevOps pipeline · Headless mode · Plugins | Full CI gate, Azure pipeline for the Testing.* services |

---

## The repo

The facilitator prepares a GitHub repo (`claudeworkshop-qa-demo`) with these pre-built projects — intentionally half-broken and with known issues planted:

- `src/Testing.SqlServer` — thin ASP.NET REST API wrapping SQL Server; has a bug: one endpoint returns `200` instead of `404` on missing record
- `src/Testing.Oracle` — same pattern for Oracle; has a validation bug: accepts empty `CorrelationId`
- `src/Testing.ServiceBus` — ASP.NET API for publishing/reading Service Bus messages; has a missing endpoint (peek without consuming)
- `tests/UITests` — Selenium + NUnit project testing a stub web UI; has a flaky wait strategy
- `tests/ApiTests` — NUnit API tests against the three services; some tests are missing, one has a genuine pagination-equivalent bug (wrong offset calculation for a paginated query result)
- `tests/DataTests` — NUnit tests with DB assertions and Service Bus checks; has a test-data isolation issue
- `CLAUDE.md` with `<!-- TODO -->` stubs
- `.claude/settings.json` with a `deny` on `appsettings.Production.json` and a stub `hooks` section
- `.temp/` and `.claude/skills/`, `.claude/commands/`, `.claude/agents/` as empty directories

---

## Part 0 — Setup & ground rules (10 min)

Same flow as the dev exercise. Fork the repo, clone, create a feature branch (`nova-none-qa-workshop`), then verify:

```bash
dotnet build
dotnet test                      # baseline — note how many pass/fail
claude --version
```

The key addition for QA: `dotnet test` baseline matters more here because some tests are *intentionally* broken. Participants note which ones fail before Claude touches anything — that list is their ground truth.

**Three rules** (same as dev, but with QA-specific colour):
- Never paste connection strings, credentials, or patient/donor data into a prompt. Real test environments have real data.
- Never `--dangerously-skip-permissions`.
- Always read the diff before accepting. A hallucinated `WHERE` clause in a database assertion is worse than a hallucinated variable name.

**Commands to lean on:** same set — `/help`, `/plan`, `/rewind`, `/clear`, `/compact`, `/context`, `/usage`, `/init`, `/memory`, `Esc`, `Shift+Tab`, `/btw`.

---

## Part 1 — Onboard and harness the repo (55 min)

### 1.1 — Get oriented, then diagram it (10 min)

Open Claude Code in the repo root. Precise prompt:

```
In 3 bullets: what this test suite covers, how it's layered,
and — tracing the path of a single API test from NUnit test method
through to the Testing.SqlServer service and back — what each layer
does and what it asserts. Read CLAUDE.md and the code first.
Don't change anything.
```

**What they should find (and couldn't have guessed from a cold start):** the API tests don't talk to SQL Server directly — they go through `Testing.SqlServer`, which handles the actual DB interaction. The test layer is intentionally decoupled from the resource. That's the QA team's architectural decision made visible.

Turn it into a diagram:

```
Create docs/api-test-flow.md with a Mermaid sequence diagram
of the path a single API test takes: NUnit test → Testing.SqlServer API
→ SQL Server → assertion. Include the HTTP verbs and assertion points.
```

**Acceptance:** diagram renders in Markdown preview, correctly shows the decoupled architecture.

### 1.2 — Critique the existing tests — no fixes yet (10 min)

```
Review ApiTests/SqlServerTests.cs and list quality issues:
missing cases, wrong assertions, HTTP status code assumptions,
or any test that could pass for the wrong reason.
Don't change anything. For each issue: the test name, what's wrong,
and what it should do.
```

**Planted issues Claude should find:** a test that asserts `IsNotEmpty` on a result but never checks the actual content; a missing `404` case; a test using a hardcoded record ID that could break on a clean environment.

Persist the findings:

```
Save these issues to .temp/known-test-issues.md
```

**Acceptance:** `.temp/known-test-issues.md` lists each issue. Participants keep this for Part 2.4 — fixing the tests one by one.

### 1.3 — Plan mode: look before you leap (10 min)

```
/plan

I want to add tests for the Testing.Oracle service's
CorrelationId validation — specifically the case where
CorrelationId is empty or null. What should we add to CLAUDE.md
so a new team member understands our test patterns?
Just propose it — don't write anything yet.
```

Participants steer the plan before approving: *"Keep it focused on test anatomy — Arrange/Act/Assert, NUnit attributes, how we use the Testing.* services. Nothing about build tooling."*

**Acceptance:** participants see and shape the intent before any file changes. The plan/thinking separation is the lesson.

### 1.4 — Make context permanent in CLAUDE.md (15 min)

Open `CLAUDE.md`. Notice the `@import .claude/qa-standards.md` line (team test standards — naming conventions, assertion style, NUnit attributes, data isolation rules — kept separate from project facts) and the `<!-- TODO -->` stubs.

Run `/init` and review. Keep `@import` and section headings (`Test Architecture`, `Service Layer`, `Test Patterns`, `Data Isolation`). Accept what's factually confirmed; `/rewind` if it strays.

If `/init` is weak, fill deliberately:

```
Explore the project and fill in the Test Architecture, Service Layer,
and Test Patterns sections of CLAUDE.md. Factual only — things you
confirmed by reading the code. For Test Patterns, look at
ApiTests/SqlServerTests.cs and capture:
- the Arrange/Act/Assert structure
- how we call the Testing.* services (HttpClient setup)
- the NUnit [TestCase] and [OneTimeSetUp] patterns
- how we assert on HTTP responses vs. database state
```

Review and correct. Then prove it loads:

```
/clear

What HTTP status code should we assert when requesting
a record that doesn't exist in Testing.SqlServer?
```

**Acceptance:** Claude answers correctly (404) from `CLAUDE.md` alone, after context wipe.

### 1.5 — Permissions are a hard rule, not advice (5 min)

Same exercise as the dev version, but the protected file is `appsettings.Production.json` in one of the Testing.* services:

```
Add a comment to src/Testing.SqlServer/appsettings.Production.json.
```

**Acceptance:** blocked by the `deny` rule. Discussion point specific to QA: in the original exercise, the wrong status code was bad; here, modifying a production config that points to the live DB or real Service Bus namespace is a live-data incident risk.

### 1.6 — Wire up a local MCP server (5 min)

Same Microsoft Learn MCP — but the prompt is QA-relevant:

```
Using the microsoft-docs MCP, look up how NUnit's [SetUp],
[OneTimeSetUp], and [TearDown] attributes interact with
parallel test execution. Summarise when each fires and
the risks in a parallel NUnit suite.
```

**Acceptance:** `/mcp` shows `microsoft-docs` connected, Claude returns accurate NUnit parallelism guidance from official docs. Participants will use this knowledge in Part 2 when writing parallel-safe tests.

---

## Part 2 — Ship a feature, then fix a real bug (65 min)

**Goal:** Run the full agentic loop twice — once to *add a missing test scenario*, once to *debug and fix a real broken test*. Hooks auto-run `dotnet test` on every test-file edit, so the red/green cycle is immediate.

### 2.1 — Plan the new test scenario (10 min)

Optional Jira start (Atlassian Rovo MCP) — facilitator provides a throwaway `NOVA-####` story: *"Testing.ServiceBus API: missing peek endpoint and no test coverage for message ordering."*

Without Jira, use the prompt directly:

```
/plan

I need to add test coverage for the Testing.ServiceBus peek endpoint:
an endpoint that reads the next message without consuming it.
The endpoint doesn't exist yet in Testing.ServiceBus — we need both
the endpoint in the service AND the NUnit tests for it.
Walk me through the plan — list every file to create or modify —
before writing a line of code.
```

Steer before approving: *"Plan the service endpoint first, then the test — same order we'd do it manually."*

**Acceptance:** plan separates service work from test work; participants approve it.

### 2.2 — Build it (20 min)

#### 2.2.1 — The Testing.ServiceBus endpoint

```
Add the peek endpoint to Testing.ServiceBus:
- GET /api/servicebus/peek?queueName={name}
- Returns 200 with the next message body (without consuming it)
- Returns 204 if the queue is empty
- Returns 404 if the queue doesn't exist
Don't add tests yet.
```

> Commit checkpoint. Then `/clear`.

#### 2.2.2 — The NUnit tests

```
Add NUnit tests for the peek endpoint in ApiTests/ServiceBusTests.cs:
- Peek on a non-empty queue returns 200 and the expected message body
- Peek is non-destructive (a second peek returns the same message)
- Peek on an empty queue returns 204
- Peek on a non-existent queue returns 404
Follow our existing test patterns from CLAUDE.md.
```

**Watch the PostToolUse hook fire** `dotnet test` as soon as the test file is saved. If tests are red, let Claude self-correct from the hook output.

> Commit checkpoint.

#### 2.2.3 — Validation

```
Add validation to the peek endpoint:
- queueName must not be empty or whitespace
- queueName must be 3–260 characters (Azure Service Bus limit)
Return 400 with a descriptive message on validation failure.
Add tests for both validation cases.
```

> Commit checkpoint.

### 2.3 — Database assertion layer (10 min)

This is the QA-specific extension beyond what the dev exercise does — testing that the Testing.SqlServer API actually persists what it claims to:

```
Add tests in DataTests/SqlServerDataTests.cs that:
- Call POST /api/records through Testing.SqlServer
- Then query the database directly (using the existing DbHelper)
  to confirm the row was actually written with the correct values
- Then call DELETE and confirm the row is gone
Follow the data isolation pattern in CLAUDE.md — wrap each test
in a transaction or clean up explicitly so tests don't bleed.
```

**Acceptance:** tests pass and each one cleans up after itself. The isolation pattern from CLAUDE.md is visible in the generated code.

### 2.4 — Fix the three known test issues, one at a time (15 min)

Open `.temp/known-test-issues.md` from 1.2 and fix each issue individually:

```
Earlier we found quality issues in ApiTests/SqlServerTests.cs.
Fix them one at a time, verifying green after each:
1) The test that asserts IsNotEmpty but never checks actual content —
   make it assert the specific expected field value.
2) The missing 404 test — add it.
3) The test with a hardcoded record ID — replace it with a setup
   that inserts the record and uses the returned ID.
Each fix should touch only the specific test, nothing else.
```

**Acceptance:** all tests green after each fix. No unrelated changes.

### 2.5 — The debugging loop on a real broken test (10 min)

There's a genuine bug in `DataTests` — a test for a paginated query result calculates the expected offset wrong. Surface it:

```
Users report DataTests/OracleDataTests.cs:GetRecords_Page2_ReturnsCorrectRows
is consistently returning the wrong records. Help me find the cause.
Don't fix it yet — show me the exact line where the expectation
and the actual calculation disagree.
```

Claude should find that the test calculates the expected skip as `page * pageSize` (0-based) while the Testing.Oracle service uses `(page - 1) * pageSize` (1-based). Then:

```
Before fixing, confirm the test is red by running it in isolation.
Then fix the offset calculation in the test so it matches
the API's documented 1-based paging behaviour.
```

**Acceptance:** the test goes green; all other tests stay green. Red → green with a confirmed root cause.

> Commit. Draft PR description to `.temp/pr-description.md` using the same prompt as the dev exercise.

> Optional Jira close: transition the story and add a comment summarising what was built and fixed.

### 2.6 — Generate the CI/CD pipeline (10 min)

Same structure as the dev exercise, but the pipeline runs the QA test suite:

```
Using the microsoft-docs MCP, look up the Azure DevOps pipeline tasks
for running NUnit tests with dotnet test and publishing TRX results.
Then create azure-pipelines.yml that:
- triggers on main and any feature/* branch
- restores and builds all projects
- runs UITests, ApiTests, and DataTests in separate steps
  (so each has its own test result published)
- publishes TRX results as test attachments
- fails the pipeline if any test step fails
```

Then extend with a deploy stage for the Testing.* services:

```
Add a Deploy stage that:
- depends on the test stage
- runs only on main
- deploys Testing.SqlServer, Testing.Oracle, and Testing.ServiceBus
  as separate Azure App Service deployments
  (use variables for the service connection and app names)
```

**Acceptance:** YAML with a multi-step test stage and a deploy stage. Each Testing.* service gets its own deploy step.

---

## ☕ Break — 15 minutes

---

## Part 3 — Build your team toolkit (35 min)

### 3.1 — Get a Skill: draft your own, then install one (15 min)

**Draft from what you just did.** The participants just built a new endpoint in Testing.ServiceBus and its tests. Codify that as a recipe:

```
You just helped me add a new endpoint to a Testing.* service
with full NUnit test coverage. Capture the repeatable recipe
as a skill at .claude/skills/new-test-endpoint.md:
- the order of work (service endpoint → validation → NUnit tests
  → data assertion where applicable)
- the NUnit patterns from our CLAUDE.md
- how to handle test data isolation for DB-touching tests
- correct HTTP codes for each scenario (200/204/400/404)
Add a description line so it auto-loads when adding a new test endpoint.
```

**Test the skill:**
```
How should I add test coverage for a new endpoint
in Testing.Oracle that creates a record?
```

Claude should invoke the skill and give step-by-step guidance without the participant re-explaining the context.

**Install a published skill** (same `/plugin marketplace add anthropics/skills` flow) — a natural pick for this team is the `pdf` skill (generate a PDF test report from a test run summary) or browse for a test-documentation skill.

### 3.2 — Author a slash command (10 min)

The QA team's recurring need is reviewing test quality before committing. Create `.claude/commands/review-tests.md`:

```markdown
Review the current git diff against our QA team standards:
- Every test follows Arrange/Act/Assert
- Test names match MethodName_Scenario_ExpectedResult pattern
- No hardcoded IDs or environment-specific values
- Data-touching tests clean up after themselves (transaction or explicit delete)
- HTTP assertion checks both status code AND response body where relevant
- No Thread.Sleep — timing must use proper waits or polling
- No commented-out test code left in
- Tests that call Testing.* services use HttpClient correctly (no raw URLs)
Report issues as a checklist. Do not modify any files.
```

**Acceptance:** `/review-tests` runs on demand and checks QA-specific standards, not just general code quality.

### 3.3 — Add a verification hook (10 min)

**1. Add the deny rule** (same as dev — deny `git push`).

**2. Add the auto-test hook.** The repo already has a stub. Extend `.claude/settings.json` to fire `dotnet test` scoped to the changed project when a test file is saved:

```json
{
  "matcher": "Edit(tests/.*Tests\\.cs)",
  "hooks": [
    {
      "type": "command",
      "command": "dotnet test --filter FullyQualifiedName~$(echo $CLAUDE_TOOL_INPUT_PATH | sed 's|tests/||;s|/.*||') --no-build --nologo 2>&1 | tail -10 || true"
    }
  ]
}
```

(Simpler fallback: `dotnet test --no-build --nologo 2>&1 | tail -10 || true` on any test file edit.)

**3. Test it:** make a small test edit; watch the hook run `dotnet test` automatically in the output. Then confirm `git push` is blocked.

**Optional — PreToolUse: block connection strings before they're written.** Same concept as the dev exercise's secret-blocking hook, but QA-flavoured — the real risk is connection strings to test databases accidentally committed:

```js
// .claude/hooks/block-connection-strings.mjs
const input = JSON.parse(await new Response(process.stdin).text());
const { content = "" } = input.tool_input ?? {};
const patterns = [
  /Server=.*;Database=.*;User Id=/i,   // SQL Server connection string
  /Data Source=.*;User Id=/i,           // Oracle connection string
  /Endpoint=sb:\/\//i                   // Service Bus connection string
];
if (patterns.some(re => re.test(content))) {
  console.error("Blocked: looks like a connection string — use configuration / environment variables instead.");
  process.exit(2);
}
process.exit(0);
```

Test it: ask Claude to write a hardcoded SQL Server connection string into a test file. The hook intercepts and Claude self-corrects.

---

## Part 4 — Orchestrate sub-agents to ship a feature (25 min)

**Goal:** deliver a complete new Testing.Oracle endpoint — plus full test coverage — using a team of sub-agents. Same sequential-then-parallel pattern as the dev exercise.

**The feature:** Testing.Oracle needs a batch-insert endpoint: `POST /api/oracle/records/batch` accepting an array of records, returning `201` with created IDs, `400` on validation failure, `207` (Multi-Status) if some records fail constraint checks.

Create three agents under `.claude/agents/`:

`.claude/agents/service-implementer.md`
```markdown
---
name: service-implementer
description: Implements new endpoints in Testing.* services — model, validation, controller.
tools: Read, Edit, Write
---
You implement Testing.* service endpoints following our standards in CLAUDE.md.
Minimal, layered changes. Correct HTTP codes. No test code — that belongs to test-writer.
```

`.claude/agents/test-writer.md`
```markdown
---
name: test-writer
description: Writes and runs NUnit tests for Testing.* service endpoints. Use after implementation.
tools: Bash, Read, Edit, Write
model: claude-haiku-4-5
---
You write NUnit + HttpClient tests following our CLAUDE.md test patterns.
Include data isolation. Run dotnet test after writing. Fix failures.
Report what passed, what failed, what you fixed.
```

`.claude/agents/qa-reviewer.md`
```markdown
---
name: qa-reviewer
description: Read-only. Reviews test coverage and service implementation against QA standards. Never edits.
tools: Read, Bash
---
Review the diff against our qa-standards.md and CLAUDE.md.
Output a checklist: missing test cases, wrong assertions, isolation issues, HTTP code correctness.
Do not modify files.
```

Orchestrate:

```
/plan

Add a batch-insert endpoint to Testing.Oracle:
POST /api/oracle/records/batch — accepts an array of records,
returns 201 with created IDs on full success,
400 if the array is empty or any record fails validation,
207 Multi-Status if some records fail Oracle constraint checks.

Plan first, then:
- use service-implementer for the endpoint, validation, and controller
- use test-writer to write and run NUnit tests covering all three response scenarios,
  plus data isolation (each test run cleans up inserted records)
- use qa-reviewer to check the final diff

List the plan before touching anything.
```

**Acceptance:** endpoint ships, all tests pass, reviewer returns actionable checklist.

**Parallel review fan-out** (same pattern as dev exercise):

```
Review the batch-insert changes with three qa-reviewer agents
running in parallel, each focused on one concern:
(1) test data isolation and cleanup
(2) HTTP status code correctness including 207 handling
(3) input validation completeness (empty array, null fields, oversize batch)
Run them concurrently, then merge findings into one prioritised checklist.
```

> Final commit checkpoint. Run `/cost` to see total spend. Run `git log --oneline` — clean milestone history.

---

## Wrap-up (5 min)

Same message as dev: the loop is identical whether it took 30 seconds or 3 hours — you don't close it until it's green, and you stay in charge. For QA engineers the specific added weight: **a hallucinated assertion that always passes is worse than a failing test** — the skill is not just making Claude write tests, but making Claude write tests that can actually catch real bugs.

---

## Optional extras

All of these are direct QA equivalents of the dev extras:

**Run a headless CI gate** (same as dev, but scoped to the test suite):
```bash
claude -p "Run the full test suite and summarise failures by project." \
  --allowedTools Bash,Read --max-turns 5
echo "exit code: $?"
```

**Make Selenium tests smarter — remove Thread.Sleep.** The UITests project has a flaky wait. Have Claude replace all `Thread.Sleep` calls with proper explicit waits using WebDriverWait:
```
/plan
Find every Thread.Sleep in UITests/ and replace them with
WebDriverWait + ExpectedConditions. Plan the replacements first.
Run the tests after each change and confirm no new failures.
```

**Close the loop against a running service.** Have Claude start Testing.SqlServer, exercise it over HTTP, and assert the responses — no unit test scaffolding, just raw HTTP assertions:
```
Start Testing.SqlServer with dotnet run in the background.
Exercise it with curl:
- POST a record and capture the returned ID
- GET it back and confirm the fields match
- GET a non-existent ID and confirm 404
- DELETE the record and confirm 204
Show me the actual requests and JSON responses, then stop the server.
```

**Action PR review comments** via GitHub MCP (same as dev — read comments, map to fixes, commit per comment, reply to thread).

**Deploy Testing.* services via Azure DevOps MCP** (same as dev but deploys three App Services instead of one; use the pipeline from 2.6).

**Add a `/test-coverage-report` command** that asks Claude to list every endpoint across the three Testing.* services and compare them against existing test coverage, outputting a gap table.

**Add the `Stop` hook** (same as dev) — notifies when Claude finishes so QA engineers review the diff before committing:
```json
"Stop": [
  { "hooks": [ { "type": "command", "command": "echo \"✅ Claude finished — check dotnet test and review the diff.\"" } ] }
]
```

**Bundle the toolkit as a plugin** and have a partner install it on a fresh clone — the onboarding test: *"Orient me in this QA codebase and tell me what test coverage is missing for Testing.ServiceBus."*

---

## Key differences from the dev exercise — summary for the facilitator

The structure, pacing, and concept coverage are identical. What changes:

- The **repo** is a QA automation codebase: NUnit test projects + three Testing.* ASP.NET services, not a domain API.
- Part 2 is **two loops** (add missing tests + fix a broken test) rather than (add a feature + fix a bug) — the feature being "added" is a new endpoint in a Testing.* service that exists to support tests.
- Part 2.3 adds a **database assertion sub-exercise** (no equivalent in the dev exercise) — testing that the persistence layer actually persisted what the API said it did.
- The Skill (3.1) codifies **test-endpoint recipes** rather than domain-model recipes.
- The Slash command (3.2) checks **QA-specific standards** (isolation, assertion correctness, no `Thread.Sleep`) not general clean-code standards.
- The PreToolUse hook (3.3 optional) blocks **connection strings** rather than API keys.
- Part 4's feature is a **batch endpoint with 207 handling** — realistic QA complexity around multi-status responses and test data cleanup.
- The UITests / Selenium angle lives entirely in optional extras — it's too environment-heavy for the main lab but gives Selenium writers something concrete to dig into.