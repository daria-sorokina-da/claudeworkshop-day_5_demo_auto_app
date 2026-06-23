import { useState } from 'react';
import { api } from '../api/client';

const STYLES  = ['elegant', 'wild', 'mythical', 'celtic', 'nature'];
const GENDERS = ['male', 'female', 'neutral'];

const STYLE_DESCRIPTIONS: Record<string, string> = {
  elegant:  'Refined and noble names fit for royalty',
  wild:     'Bold, untamed names for free spirits',
  mythical: 'Names drawn from legend and lore',
  celtic:   'Ancient Irish and Scottish heritage names',
  nature:   'Inspired by the natural world',
};

export default function NameGenerator() {
  const [style, setStyle]   = useState('elegant');
  const [gender, setGender] = useState('neutral');
  const [names, setNames]   = useState<string[]>([]);
  const [loading, setLoading] = useState(false);
  const [copied, setCopied]   = useState<string | null>(null);

  async function generate() {
    setLoading(true);
    try {
      const res = await api.names.generate(style, gender);
      setNames(res.names);
    } finally {
      setLoading(false);
    }
  }

  async function copy(name: string) {
    await navigator.clipboard.writeText(name);
    setCopied(name);
    setTimeout(() => setCopied(null), 1500);
  }

  return (
    <div>
      <h1 className="page-title">Horse Name Generator</h1>
      <p className="page-sub">Pick a style and gender to generate the perfect horse name.</p>

      <div className="gen-controls">
        <div className="control-group">
          <label>Style</label>
          <div className="pill-group">
            {STYLES.map(s => (
              <button
                key={s}
                className={`pill${style === s ? ' selected' : ''}`}
                onClick={() => setStyle(s)}
                title={STYLE_DESCRIPTIONS[s]}
              >
                {s}
              </button>
            ))}
          </div>
          <p style={{ fontSize: '.8rem', color: '#888', marginTop: '.4rem' }}>
            {STYLE_DESCRIPTIONS[style]}
          </p>
        </div>

        <div className="control-group">
          <label>Gender</label>
          <div className="pill-group">
            {GENDERS.map(g => (
              <button
                key={g}
                className={`pill${gender === g ? ' selected' : ''}`}
                onClick={() => setGender(g)}
              >
                {g}
              </button>
            ))}
          </div>
        </div>

        <button className="gen-btn" onClick={generate} disabled={loading}>
          {loading ? 'Generating…' : '✨ Generate Names'}
        </button>
      </div>

      {names.length > 0 && (
        <div className="name-cards">
          {names.map(name => (
            <div key={name} className="name-card">
              <span>{name}</span>
              <button className="copy-btn" onClick={() => copy(name)}>
                {copied === name ? '✓' : 'Copy'}
              </button>
            </div>
          ))}
        </div>
      )}
    </div>
  );
}
