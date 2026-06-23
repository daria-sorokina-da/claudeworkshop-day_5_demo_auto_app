import { useState } from 'react';
import Encyclopedia from './pages/Encyclopedia';
import NameGenerator from './pages/NameGenerator';
import PersonalityQuiz from './pages/PersonalityQuiz';
import './App.css';

type Page = 'encyclopedia' | 'names' | 'quiz';

const NAV: { id: Page; label: string; emoji: string }[] = [
  { id: 'encyclopedia', label: 'Breed Encyclopedia', emoji: '📖' },
  { id: 'names',        label: 'Name Generator',     emoji: '✨' },
  { id: 'quiz',         label: 'Which Breed Are You?', emoji: '🐴' },
];

export default function App() {
  const [page, setPage] = useState<Page>('encyclopedia');

  return (
    <div className="app">
      <header className="header">
        <div className="header-inner">
          <span className="logo">🐎 HorseWorld</span>
          <nav className="nav">
            {NAV.map(n => (
              <button
                key={n.id}
                className={`nav-btn${page === n.id ? ' active' : ''}`}
                onClick={() => setPage(n.id)}
              >
                {n.emoji} {n.label}
              </button>
            ))}
          </nav>
        </div>
      </header>

      <main className="main">
        {page === 'encyclopedia' && <Encyclopedia />}
        {page === 'names'        && <NameGenerator />}
        {page === 'quiz'         && <PersonalityQuiz />}
      </main>
    </div>
  );
}
