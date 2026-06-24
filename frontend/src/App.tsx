import { BrowserRouter, Routes, Route, useNavigate, useLocation } from 'react-router-dom';
import Encyclopedia from './pages/Encyclopedia';
import NameGenerator from './pages/NameGenerator';
import PersonalityQuiz from './pages/PersonalityQuiz';
import './App.css';

const NAV: { path: string; testId: string; label: string; emoji: string }[] = [
  { path: '/',      testId: 'nav-encyclopedia', label: 'Breed Encyclopedia',    emoji: '📖' },
  { path: '/names', testId: 'nav-names',        label: 'Name Generator',        emoji: '✨' },
  { path: '/quiz',  testId: 'nav-quiz',         label: 'Which Breed Are You?',  emoji: '🐴' },
];

function AppShell() {
  const navigate = useNavigate();
  const { pathname } = useLocation();

  return (
    <div className="app">
      <header className="header">
        <div className="header-inner">
          <span className="logo">🐎 HorseWorld</span>
          <nav className="nav">
            {NAV.map(n => (
              <button
                key={n.path}
                className={`nav-btn${pathname === n.path ? ' active' : ''}`}
                onClick={() => navigate(n.path)}
                data-testid={n.testId}
              >
                {n.emoji} {n.label}
              </button>
            ))}
          </nav>
        </div>
      </header>

      <main className="main">
        <Routes>
          <Route path="/" element={<Encyclopedia />} />
          <Route path="/names" element={<NameGenerator />} />
          <Route path="/quiz" element={<PersonalityQuiz />} />
        </Routes>
      </main>
    </div>
  );
}

export default function App() {
  return (
    <BrowserRouter>
      <AppShell />
    </BrowserRouter>
  );
}
