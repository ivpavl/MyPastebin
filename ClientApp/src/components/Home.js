import React, { useRef, useState } from 'react';
import '../styles/Home.css';

function Home() {
  const [error, setError] = useState({errorOccured: false, errorMessage: ''});
  const userNameRef = useRef();
  const textBlockRef = useRef();
  const jwtTokenInputForm = useRef();

  async function createNewTextBlock() {
    try {
      let res = await fetch('textblock/add', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json;charset=utf-8',
        },
        body: JSON.stringify({
          'UserName': userNameRef.current.value,
          'TextBlock': textBlockRef.current.value,
        })
      });

      let result = await res.json();
      if (res.ok) {
        setError({...error, errorOccured: false})
        return result;
      } else {
        setError({...error, errorOccured: true, errorMessage: res.status})
      }
    } catch (error) {
      setError({...error, errorOccured: true, errorMessage: error})
      console.error(error);
    }
  }

  async function getInfoUsingJWT() {
    try {
      let res = await fetch('/user/', {
        method: 'GET',
        headers: {
          'Content-Type': 'application/json;charset=utf-8',
          'Authorization': 'Bearer ' + jwtTokenInputForm.current.value
        }
      });

      let result = await res.json();
      if (res.ok) {
        return result;
      }
    } catch (error) {
      console.error(error);
    }
  }


  const handleCreateTextBlock = async (event) => {
    event.preventDefault();
    let postId = await createNewTextBlock();
    if(postId)
    {
      window.location.href += 'post?id=' + postId;
    } else
    {
      console.log("No post id found!");
    }
  }

  const handleJWTAttempt = async (event) => {
    event.preventDefault();
    let postId = await getInfoUsingJWT();
  }


  return (
    <div>
      
      {
        error.errorOccured && <p>An error occurred. Please try again. {error.errorMessage}</p>
      }
      <h3>Welcome to MyPastebin!</h3>
      
      <form onSubmit = {handleCreateTextBlock} className='createTextBox'>
        <label>Add new note!</label>
        <input type='text'
          ref={userNameRef}
          placeholder='Your name'
          />
        <textarea 
          placeholder='Your text'
          ref={textBlockRef}
        />
        <button type='submit'>Submit!</button>
      </form>

      <form onSubmit = {handleJWTAttempt} className='createTextBox'>
        <input type='text'
          ref={jwtTokenInputForm}
          placeholder='Your JWT TOKEN'
          />
        <button type='submit'>Submit!</button>
      </form>

    </div>
  );
}

export default Home;
