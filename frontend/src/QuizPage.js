import React, { useEffect, useState } from 'react';
import axios from 'axios';

const QuizPage = () => {
  const [questions, setQuestions] = useState([]);
  const [error, setError] = useState('');
  const [success, setSuccess] = useState('');
  const [answers, setAnswers] = useState({});
  const [submitError, setSubmitError] = useState('');
  const [email, setEmail] = useState('');

  useEffect(() => {
    axios.get('http://localhost:5206/api/questionnaire')
      .then(response => {
        setQuestions(response.data);
      })
      .catch(err => {
        setError('Error fetching data: ' + err.message);
      });
  }, []);

  const setSelectedAnswer = (questionId, answer) => {
    setSuccess('')
    setAnswers((prevAnswers) => ({
      ...prevAnswers,
      [questionId]: answer,
    }));
  };

  const submitAnswers = (e) => {
    setSubmitError('');
    setSuccess('')
    e.preventDefault();

    if (!email) {
      setSubmitError('Email is required')
      return
    }

    axios.post('http://localhost:5206/api/questionnaire', { answers, email })
      .then(response => {
        setSuccess(`Submission successfull! Your score: ${response.data.totalScore}`)
        setEmail('')
        setAnswers({})
      })
      .catch(err => {
        console.error(err.message)
        setSubmitError(`Error submitting answers: ${err.message}`);
      });
  };

  return (
    <div className='mx-10 mb-20'>
      <h1 className='font-bold text-center mb-4'>Questionnaire</h1>
      {error ? (
        <p>{error}</p>
      ) : (
        <form onSubmit={submitAnswers} className="flex flex-col gap-4">
          {questions.map((q, index) => (
            <div key={index} className='flex flex-col'>
              <span className='font-bold'>{index + 1}. {q.question}</span>
              {q.type === 2 && (
                <textarea
                  className='border border-black'
                  onChange={(e) => setSelectedAnswer(q.id, [e.target.value])}
                />
              )}
              {q.type === 1 && (
                <div className='flex flex-col gap-1'>
                  {q.options.map((x) => (
                    <label key={x} className='flex gap-1'>
                      <input
                        type="radio"
                        value={x}
                        name={q.id}
                        onChange={() => setSelectedAnswer(q.id, [x])}
                      />
                      <span>{x}</span>
                    </label>
                  ))}
                </div>
              )}
              {q.type === 0 && (
                <div className='flex flex-col gap-1'>
                  {q.options.map((x) => (
                    <label key={x} className='flex gap-1'>
                      <input
                        type="checkbox"
                        value={x}
                        name={q.id}
                        onChange={(e) => {
                          const selectedOptions = answers[q.id] || [];
                          if (e.target.checked) {
                            setSelectedAnswer(q.id, [...selectedOptions, x]);
                          } else {
                            setSelectedAnswer(q.id, selectedOptions.filter(option => option !== x));
                          }
                        }}
                      />
                      <span>{x}</span>
                    </label>
                  ))}
                </div>
              )}
            </div>
          ))}
          <div>
            <label htmlFor="email">Email: </label>
            <input
              type="email"
              id="email"
              value={email}
              onChange={(event) => setEmail(event.target.value)}
              placeholder="Enter your email"
              className='border border-black p-2'
            />
          </div>
          <div className="flex flex-col justify-center mt-6">
            <button
              type="submit"
              className="bg-blue-500 text-white py-2 px-4 rounded"
            >
              Submit
            </button>
            { submitError && 
              <span className='text-[#cc1414]'>{submitError}</span>
            }
            { success && 
              <span className='text-[#21b321]'>{success}</span>
            }
          </div>
        </form>
      )}
    </div>
  );
};

export default QuizPage;
