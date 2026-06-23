import type {
  Breed,
  BreedSummary,
  NameGenerateResponse,
  PersonalityQuestion,
  PersonalityResult,
  QuizAnswerResponse,
  QuizQuestion,
} from './types';

const base = '/api';

async function get<T>(path: string): Promise<T> {
  const res = await fetch(`${base}${path}`);
  if (!res.ok) throw new Error(`GET ${path} failed: ${res.status}`);
  return res.json();
}

async function post<T>(path: string, body: unknown): Promise<T> {
  const res = await fetch(`${base}${path}`, {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(body),
  });
  if (!res.ok) throw new Error(`POST ${path} failed: ${res.status}`);
  return res.json();
}

export const api = {
  breeds: {
    list: ()                          => get<BreedSummary[]>('/breeds'),
    get:  (id: string)                => get<Breed>(`/breeds/${id}`),
    quizQuestion: ()                  => get<QuizQuestion>('/breeds/quiz/question'),
    quizAnswer: (questionId: string, answerId: string) =>
      post<QuizAnswerResponse>('/breeds/quiz/answer', { questionId, answerId }),
  },
  names: {
    generate: (style: string, gender: string) =>
      get<NameGenerateResponse>(`/names/generate?style=${style}&gender=${gender}`),
  },
  personality: {
    questions: () => get<PersonalityQuestion[]>('/personality/questions'),
    result: (answers: { questionId: string; optionId: string }[]) =>
      post<PersonalityResult>('/personality/result', { answers }),
  },
};
