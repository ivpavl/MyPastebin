import React, { useRef, useState } from 'react';
import '../styles/Home.css';

function Login() {
  const userNameRef = useRef();
  const textBlockRef = useRef();

  async function tryLogging() {
    try {
      let res = await fetch('user/login', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json;charset=utf-8',
        },
        body: JSON.stringify({
          'UserName': userNameRef.current.value,
          'Password': textBlockRef.current.value,
        })
      });

      let result = await res.json();
      if (res.ok) {
        return result;
      }
    } catch (error) {
      console.error(error);
    }
  }


  const handleLogin = async (event) => {
    event.preventDefault();
    let postId = await tryLogging();
  }


  return (
    <div>
      
      <h3>Welcome to MyPastebin!</h3>
      
      <form onSubmit = {handleLogin} className='createTextBox'>
        <label>Try logging in!</label>
        <input type='text'
          ref={userNameRef}
          placeholder='Your name'
          />
        <textarea 
          placeholder='Your password'
          ref={textBlockRef}
        />
        <button type='submit'>Submit!</button>
      </form>


    </div>
  );
}

export default Login;
