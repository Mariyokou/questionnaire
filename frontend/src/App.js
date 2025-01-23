import React, { useState } from 'react';
import QuizPage from './QuizPage';
import ScorePage from './ScorePage';
import className from 'classname';

const App = () => {
  const [activeTab, setActiveTab] = useState('quiz');

  return (
    <div>
      <div className='flex mb-5'>
        <button 
          onClick={() => setActiveTab('quiz')} 
          className={className(
            'px-3 py-6 cursor-pointer',
            activeTab === 'quiz' 
              ? 'text-white bg-[#4CAF50]' 
              : 'text-black bg-[#ddd]'
          )}
        >
          Solve quiz
        </button>

        <button 
          onClick={() => setActiveTab('score')} 
          className={className(
            'px-3 py-6 cursor-pointer',
            activeTab === 'score' 
              ? 'text-white bg-[#4CAF50]' 
              : 'text-black bg-[#ddd]'
          )}
        >
          High scores
        </button>
      </div>

     {activeTab === 'quiz' && <QuizPage />}
     {activeTab === 'score' && <ScorePage />}
    </div>
  );
};

export default App;
