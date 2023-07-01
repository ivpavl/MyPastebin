import React, { useState, useEffect } from 'react';
import tokenHandler from '../types/handlers';

interface IError
{
  isError: boolean,
  errorMessage: string
}

function Post() {
  const tokenHandle = new tokenHandler();
  const [textBlock, setTextBlock] = useState<ITextBlock>({id: 0, title: 'Loading', text: '', hashId: '',});
  const [error, setError] = useState<IError>({isError: false, errorMessage: ''});
  let params = new URLSearchParams(window.location.search);
  let postId:string = params.get('id') || '';

  async function getPostById<ITextBlock>(postId:string) {
    try {
      return await fetch('textblock?id=' + postId, {
        method: 'GET',
        headers: {
          'Content-Type': 'application/json;charset=utf-8',
          ...tokenHandle.getAuthorization()
        },
      })
      .then((res) =>
      {
        if(res.ok) {
          return res.json()
        }
        return undefined;
      })
      .then((json) => {
        return json;
      });
    } catch (error) {
      console.error(error);
      return undefined;
    }
  }

  
  useEffect(() => {
    if (postId) {
      getPostById(postId)
      .then((tb) => {
        if(tb != undefined) {
          setTextBlock(tb);
        }
        else {
          setError({isError: true, errorMessage: "Unable to find requested text."});
        }
      });
    }

  }, []);

  return (
    <div>
      
      {
        error.isError ? <p style={{color: 'red'}}>{error.errorMessage}</p> : ""
      }

      <h1>{textBlock.title}</h1>
      <hr/>
      <p>{textBlock.text}</p>
      

    </div>
  );
}

export default Post;
