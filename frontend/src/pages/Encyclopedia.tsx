import { useEffect, useState } from 'react';
import { api } from '../api/client';
import type { Breed, BreedSummary, QuizAnswerResponse, QuizQuestion } from '../api/types';

type Tab = 'browse' | 'quiz';

export default function Encyclopedia() {
  const [tab, setTab] = useState<Tab>('browse');
  const [breeds, setBreeds] = useState<BreedSummary[]>([]);
  const [selected, setSelected] = useState<Breed | null>(null);
  const [loadingBreeds, setLoadingBreeds] = useState(true);
  const [error, setError] = useState('');

  useEffect(() => {
    api.breeds.list()
      .then(setBreeds)
      .catch(() => setError('Failed to load breeds.'))
      .finally(() => setLoadingBreeds(false));
  }, []);

  async function openDetail(id: string) {
    const breed = await api.breeds.get(id);
    setSelected(breed);
  }

  return (
    <div>
      <h1 className="page-title">Breed Encyclopedia</h1>
      <p className="page-sub">Explore horse breeds from around the world, or test your knowledge.</p>

      <div className="tabs">
        <button className={`tab-btn${tab === 'browse' ? ' active' : ''}`} onClick={() => setTab('browse')}>
          Browse Breeds
        </button>
        <button className={`tab-btn${tab === 'quiz' ? ' active' : ''}`} onClick={() => setTab('quiz')}>
          Identification Quiz
        </button>
      </div>

      {tab === 'browse' && (
        <>
          {loadingBreeds && <p className="loading">Loading breeds…</p>}
          {error && <p className="error">{error}</p>}
          <div className="card-grid">
            {breeds.map(b => (
              <div key={b.id} className="card" onClick={() => openDetail(b.id)}>
                <img src={b.imageUrl} alt={b.name} loading="lazy" />
                <div className="card-body">
                  <h3>{b.name}</h3>
                  <div className="origin">{b.origin}</div>
                  <span className="badge">{b.category}</span>
                </div>
              </div>
            ))}
          </div>
        </>
      )}

      {tab === 'quiz' && <IdentificationQuiz />}

      {selected && (
        <div className="detail-overlay" onClick={() => setSelected(null)}>
          <div className="detail-panel" onClick={e => e.stopPropagation()}>
            <img src={selected.imageUrl} alt={selected.name} />
            <div className="detail-content">
              <h2>{selected.name}</h2>
              <div className="sub">{selected.origin} · {selected.category} · {selected.heightRange} · {selected.weightRange}</div>
              <p>{selected.summary}</p>
              <div className="detail-section">
                <h4>Temperament</h4>
                <div className="chips">
                  {selected.temperament.map(t => <span key={t} className="chip">{t}</span>)}
                </div>
              </div>
              <div className="detail-section">
                <h4>Uses</h4>
                <div className="chips">
                  {selected.uses.map(u => <span key={u} className="chip">{u}</span>)}
                </div>
              </div>
              <div className="detail-section">
                <h4>Fun Facts</h4>
                <ul className="fun-facts">
                  {selected.funFacts.map((f, i) => <li key={i}>{f}</li>)}
                </ul>
              </div>
              <button className="close-btn" onClick={() => setSelected(null)}>Close</button>
            </div>
          </div>
        </div>
      )}
    </div>
  );
}

function IdentificationQuiz() {
  const [question, setQuestion] = useState<QuizQuestion | null>(null);
  const [result, setResult] = useState<QuizAnswerResponse | null>(null);
  const [chosen, setChosen] = useState<string | null>(null);
  const [score, setScore] = useState({ correct: 0, total: 0 });
  const [loading, setLoading] = useState(true);

  async function loadQuestion() {
    setLoading(true);
    setResult(null);
    setChosen(null);
    try {
      const q = await api.breeds.quizQuestion();
      setQuestion(q);
    } finally {
      setLoading(false);
    }
  }

  useEffect(() => { loadQuestion(); }, []);

  async function answer(breedId: string) {
    if (!question || chosen) return;
    setChosen(breedId);
    const res = await api.breeds.quizAnswer(question.questionId, breedId);
    setResult(res);
    setScore(s => ({ correct: s.correct + (res.correct ? 1 : 0), total: s.total + 1 }));
  }

  if (loading) return <p className="loading">Loading question…</p>;
  if (!question) return null;

  return (
    <div className="quiz-card">
      <p style={{ color: '#888', fontSize: '.85rem', marginBottom: '.5rem' }}>
        Identify the breed from the description:
      </p>
      <p style={{ fontSize: '1rem', fontWeight: 500 }}>{question.description}</p>
      <div className="clue-chips chips">
        {question.traitClues.map(c => <span key={c} className="chip">{c}</span>)}
      </div>
      <div className="quiz-options">
        {question.options.map(opt => {
          let cls = 'quiz-opt';
          if (chosen) {
            if (opt.breedId === result?.correctBreedId) cls += ' correct';
            else if (opt.breedId === chosen) cls += ' incorrect';
          }
          return (
            <button
              key={opt.breedId}
              className={cls}
              disabled={!!chosen}
              onClick={() => answer(opt.breedId)}
            >
              {opt.breedName}
            </button>
          );
        })}
      </div>
      {result && (
        <>
          <p className={`quiz-feedback ${result.correct ? 'ok' : 'err'}`}>
            {result.correct ? '✓ Correct!' : `✗ It was the ${result.correctBreedName}`}
          </p>
          <p className="quiz-score">Score: {score.correct} / {score.total}</p>
          <button className="next-btn" onClick={loadQuestion}>Next question →</button>
        </>
      )}
    </div>
  );
}
