import { useEffect, useState } from 'react';
import { api } from '../api/client';
import type { PersonalityQuestion, PersonalityResult } from '../api/types';

interface Answer { questionId: string; optionId: string; }

export default function PersonalityQuiz() {
  const [questions, setQuestions]   = useState<PersonalityQuestion[]>([]);
  const [current, setCurrent]       = useState(0);
  const [answers, setAnswers]       = useState<Answer[]>([]);
  const [result, setResult]         = useState<PersonalityResult | null>(null);
  const [loading, setLoading]       = useState(true);
  const [submitting, setSubmitting] = useState(false);
  const [error, setError]           = useState('');

  useEffect(() => {
    api.personality.questions()
      .then(setQuestions)
      .catch(() => setError('Failed to load quiz.'))
      .finally(() => setLoading(false));
  }, []);

  async function pick(optionId: string) {
    if (!questions[current]) return;
    const next = [...answers, { questionId: questions[current].questionId, optionId }];
    setAnswers(next);

    if (current + 1 < questions.length) {
      setCurrent(c => c + 1);
    } else {
      setSubmitting(true);
      try {
        const res = await api.personality.result(next);
        setResult(res);
      } catch {
        setError('Failed to calculate result.');
      } finally {
        setSubmitting(false);
      }
    }
  }

  function retake() {
    setCurrent(0);
    setAnswers([]);
    setResult(null);
  }

  if (loading)    return <p className="loading">Loading quiz…</p>;
  if (error)      return <p className="error">{error}</p>;
  if (submitting) return <p className="loading">Calculating your breed…</p>;

  if (result) {
    return (
      <div>
        <h1 className="page-title">You are a {result.breedName}!</h1>
        <p className="page-sub" style={{ marginBottom: '1.5rem' }}>Your personality matched this magnificent breed.</p>
        <div className="result-card">
          <img src={result.imageUrl} alt={result.breedName} />
          <div className="result-body">
            <h2>{result.breedName}</h2>
            <p>{result.description}</p>
            <div className="chips">
              {result.traits.map(t => <span key={t} className="chip">{t}</span>)}
            </div>
            <button className="retake-btn" onClick={retake}>Retake Quiz</button>
          </div>
        </div>
      </div>
    );
  }

  const q = questions[current];
  const pct = Math.round((current / questions.length) * 100);

  return (
    <div>
      <h1 className="page-title">Which Horse Breed Are You?</h1>
      <p className="page-sub">Answer {questions.length} questions to find your spirit breed.</p>

      <div className="p-quiz">
        <div className="progress-bar">
          <div className="progress-fill" style={{ width: `${pct}%` }} />
        </div>
        <p style={{ fontSize: '.8rem', color: '#888', marginBottom: '1rem' }}>
          Question {current + 1} of {questions.length}
        </p>
        <p className="p-question">{q.text}</p>
        <div className="p-options">
          {q.options.map(opt => (
            <button key={opt.optionId} className="p-opt" onClick={() => pick(opt.optionId)}>
              {opt.text}
            </button>
          ))}
        </div>
      </div>
    </div>
  );
}
