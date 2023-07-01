import React, { useRef, useState } from 'react';
import '../styles/Home.css';
import tokenHandler from '../types/handlers';

interface ILogin {
  Username: string,
  Password: string,
}
interface ILoginResponse {
  maxAge: number,
  token: string,
}
interface IMessage {
    isError: boolean,
    text: string,
  }
  


function LoginForm() {
  const [message, setMessage] = useState<IMessage>({isError: false, text: ''});
  const tokenHandle = new tokenHandler();
  const userNameRef = useRef();
  const textBlockRef = useRef();

  async function tryLogging(): Promise<ILoginResponse> {
    try {
      const loginData: ILogin = {
        Username: userNameRef.current.value ?? '',
        Password: textBlockRef.current.value ?? '',
      };

      let response = await fetch('user/login', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json;charset=utf-8',
        },
        body: JSON.stringify(loginData)
      });

      if (response.ok) {
        let responseData: ILoginResponse = await response.json();
        console.log(responseData);
        setMessage({isError: false, text: "Login successful!"});
        return responseData;
    } else {
        setMessage({isError: true, text: response.statusText});
        return {token: '', maxAge: 0}
    }
} catch (error) {
    console.error(error);
    setMessage({isError: true, text: 'Bad request'});
    return {token: '', maxAge: 0}
    }
  }


  const handleLogin = async (event: Event) => {
    event.preventDefault();
    let loginResponse: ILoginResponse = await tryLogging();
    tokenHandle.setToken(loginResponse.token, loginResponse.maxAge);
    window.location.href = '/';
  }

  return (
    <div>
      
      {
        message.text && <p style={message.isError ? {color: 'red'} : {color: 'green'}}>{message.text}</p>
      }

      <form onSubmit = {handleLogin} className='createTextBox'>
        <label>Try logging in!</label>
        <input type='text'
          ref={userNameRef}
          placeholder='Your name'
          />
        <input //type='password'
          ref={textBlockRef}
          placeholder='Your password'
        />
        <button type='submit'>Submit!</button>
      </form>

    </div>
  );
}

export default LoginForm;
