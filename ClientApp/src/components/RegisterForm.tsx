import React, { useRef, useState } from 'react';
import '../styles/Home.css';
import tokenHandler from '../types/handlers';

interface IRegister {
  Username: string,
  Password: string,
}
interface IRegisterResponse {
  maxAge: number,
  token: string,
}
interface IMessage {
  isError: boolean,
  text: string,
}




function RegisterForm() {
  const [message, setMessage] = useState<IMessage>({isError: false, text: ''});
  const tokenHandle = new tokenHandler();
  const userNameRef = useRef();
  const textBlockRef = useRef();

  async function tryRegistering(): Promise<IRegisterResponse> {
    try {
      const loginData: IRegister = {
        Username: userNameRef.current.value ?? '',
        Password: textBlockRef.current.value ?? '',
      };

      let response = await fetch('user/register', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json;charset=utf-8',
          ...tokenHandle.getAuthorization()
        },
        body: JSON.stringify(loginData)
      });

      if (response.ok) {
        let responseData: IRegisterResponse = await response.json();
        setMessage({isError: false, text: "Register successful!"});
        return responseData;
      } else {
        setMessage({isError: true, text: response.statusText});
        return {token: '', maxAge: 0};
      }
    } catch (error) {
      console.error(error);
      setMessage({isError: true, text: error.message});
      return {token: '', maxAge: 0};
    }
  }


  const handleLogin = async (event: Event) => {
    event.preventDefault();
    let loginResponse: IRegisterResponse = await tryRegistering();
    tokenHandle.setToken(loginResponse.token, loginResponse.maxAge);
    window.location.href = '/';
  }

  return (
    <div>
        {
          message.text && <p style={message.isError ? {color: 'red'} : {color: 'green'}}>{message.text}</p>
        }  

        <form onSubmit = {handleLogin} className='createTextBox'>
          <label>Register!</label>
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

export default RegisterForm;
