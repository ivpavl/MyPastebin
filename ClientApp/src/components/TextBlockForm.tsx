import React, { useRef, useState } from 'react';
import '../styles/Home.css';
import tokenHandler from '../types/handlers';


interface IMessage {
  isError: boolean,
  text: string,
}

enum TextBlockExpiration {
  '60 seconds',
  '10 minutes',
  '1 hour',
  '1 day',
  '1 month',
  'NEVER'
}

interface ITextBlock {
  Textblock: string,
  Title: string,
  ExpireIn: number,
}
interface ITextBlockCreationResponse {
  isSuccessful: boolean,
  postId: string,
}


function TextBlockForm() {
  const [message, setMessage] = useState<IMessage>({isError: false, text: ''});
  const textBlockTitleRef = useRef<HTMLInputElement>();
  const textBlockExpRef = useRef<HTMLSelectElement>();
  const textBlockRef = useRef<HTMLTextAreaElement>();

  const tokenHandle = new tokenHandler();

  async function tryCreatingTextBlock(): Promise<ITextBlockCreationResponse> {
    try {
      const loginData: ITextBlock = {
        'Textblock': textBlockRef.current.value ?? '',
        'Title': textBlockTitleRef.current.value ?? '',
        'ExpireIn': Number(textBlockExpRef.current.value ?? 5),
      };

      let response = await fetch('textblock/add', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json;charset=utf-8',
          ...tokenHandle.getAuthorization()
        },
        body: JSON.stringify(loginData)
      });

      if (response.ok) {
        let responseData: ITextBlockCreationResponse = await response.json();
        responseData.isSuccessful = true;
        setMessage({isError: false, text: "Send successful! Redirecting..."});
        return responseData;
      } else {
        setMessage({isError: true, text: response.statusText});
      }
    } catch (error) {
      console.log(error);
      setMessage({isError: true, text: "Bad request"});
    }
  }

  const handleCreateTextBlock = async (event) => {
    event.preventDefault();
    let newTextBlock: ITextBlockCreationResponse = await tryCreatingTextBlock();
    if(newTextBlock.isSuccessful)
    {
      window.location.href += 'post?id=' + newTextBlock.postId;
    }
  }

  return (
    <div>
      
      {
        message.text && <p style={message.isError ? {color: 'red'} : {color: 'green'}}>{message.text}</p>
      }
      
      <form onSubmit = {handleCreateTextBlock} className='createTextBox'>
        <label>Add new note!</label>
        <input 
          placeholder='Your title'
          ref={textBlockTitleRef}
        />
        <textarea 
          placeholder='Your text'
          ref={textBlockRef}
        />

        <label>Expire in:</label>
        <select ref={textBlockExpRef}>
          {Object.keys(TextBlockExpiration)
          .filter(key => !isNaN(Number(TextBlockExpiration[key])))
          .map((value:string, key:number) => (
            <option key={key} value={key}>
              {value}
            </option>
          ))}
        </select>

        <button type='submit'>Submit!</button>
      </form>

    </div>
  );
}

export default TextBlockForm;
