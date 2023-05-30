import React, { useState } from 'react';

function Home() {
  const [note, setNote] = useState();

  async function request() {
    let res = await fetch('post', {
      method: 'GET',
      headers: {
        'Content-Type': 'application/json;charset=utf-8'
      }
    });
    
    let result = await res.json();

    if (res.ok) {
      console.log(result);
    } else {
      alert("Ошибка HTTP: " + res.status);
    }
  }


  const handleSubmit = async (event) => {
    event.preventDefault();

    await request();

    console.log(`Form submitted, ${note}`);    
  }

  return (
    <div>
      <h1>Welcome to MyPastebin!</h1>
      
      <form onSubmit = {handleSubmit}>
        <label>Add new note!</label>
        <textarea 
            value={note} 
            onChange={e => setNote(e.target.value)}
        >

        </textarea>

        <button type="submit">Submit!</button>
      </form>
    </div>
  );
}

export default Home;
