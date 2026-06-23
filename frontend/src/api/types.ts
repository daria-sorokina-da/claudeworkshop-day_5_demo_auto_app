export interface BreedSummary {
  id: string;
  name: string;
  origin: string;
  category: string;
  summary: string;
  imageUrl: string;
}

export interface Breed extends BreedSummary {
  temperament: string[];
  uses: string[];
  funFacts: string[];
  heightRange: string;
  weightRange: string;
}

export interface QuizOption {
  breedId: string;
  breedName: string;
}

export interface QuizQuestion {
  questionId: string;
  description: string;
  traitClues: string[];
  options: QuizOption[];
}

export interface QuizAnswerResponse {
  correct: boolean;
  correctBreedId: string;
  correctBreedName: string;
}

export interface NameGenerateResponse {
  names: string[];
  style: string;
  gender: string;
}

export interface PersonalityOption {
  optionId: string;
  text: string;
}

export interface PersonalityQuestion {
  questionId: string;
  text: string;
  options: PersonalityOption[];
}

export interface PersonalityResult {
  breedId: string;
  breedName: string;
  description: string;
  traits: string[];
  imageUrl: string;
}
