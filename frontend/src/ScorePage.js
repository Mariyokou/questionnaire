import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { format } from 'date-fns';
import { FaMedal } from 'react-icons/fa'; 

const ScorePage = () => {
  const [highScores, setHighScores] = useState([]);
  const [error, setError] = useState(null);

  useEffect(() => {
    axios.get('http://localhost:5206/api/score')
      .then(response => {
        setHighScores(response.data);
      })
      .catch(err => {
        console.error(err)
        setError('Error fetching data: ' + err.message);
      });
  }, []);

  return (
    <div className='mx-10 mb-20'>
      <h1 className='font-bold text-center mb-4'>High scores</h1>
      {error ? (
        <p>{error}</p>
      ) : (
        <>
          {highScores.length === 0 && 
            <p>No high scores yet</p>
          }
          {highScores.length > 0 &&
            <table className="table-auto w-full text-left border-collapse">
              <thead>
                <tr>
                  <th>Position</th>
                  <th>Score</th>
                  <th>User</th>
                  <th>Date</th>
                </tr>
              </thead>
              <tbody>
                {highScores.map((scoreData, index) => {
                  let medalIcon = null;

                  if (index === 0) {
                    medalIcon = <FaMedal className="text-yellow-500" />;
                  } else if (index === 1) {
                    medalIcon = <FaMedal className="text-gray-400" />;
                  } else if (index === 2) {
                    medalIcon = <FaMedal className="text-orange-500" />;
                  }

                  return (
                    <tr key={index}>
                      <td className='flex items-center gap-1 '>{medalIcon}{index+1}{index === 0 
                        ? 'st' 
                        : index === 1 
                          ? 'nd' 
                          : index === 2 
                            ? 'rd' 
                            : 'th'
                      }</td>
                      <td>{scoreData.totalScore}</td>
                      <td>{scoreData.email}</td>
                      <td>{scoreData.date ? format(scoreData.date, 'yyyy-MM-dd HH:mm') : '-'}</td>
                    </tr>
                  )
                })}
              </tbody>
            </table>
          }
        </>
      )}
    </div>
  );
};

export default ScorePage;
