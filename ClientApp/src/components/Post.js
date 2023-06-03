import React, { useState, useEffect } from 'react';

function Post() {
  const [textBlock, setTextBlock] = useState('Loading...');

  async function getPostById(postId) {
    try {
      let res = await fetch('textblock?id=' + postId, {
        method: 'GET',
        headers: {
          'Content-Type': 'application/json;charset=utf-8',
          'Authorization': 'Bearer 133',
        },
      });

      if (res.ok) {
        let result = await res.json();
        console.log(result);
        return result['textBlock'];
      } else {
        return 'Not available!';
      }
    } catch (error) {
      console.error(error);
      return 'Error occurred while fetching the post!';
    }
  }

  useEffect(() => {
    let params = new URLSearchParams(window.location.search);
    let postId = params.get('id');

    if (postId) {
      getPostById(postId)
        .then((postTextBlock) => {
          setTextBlock(postTextBlock);
        })
        .catch((error) => {
          console.error(error);
          setTextBlock('Error occurred while fetching the post!');
        });
      }
      else {
        setTextBlock('No post by this Id found!');
    }
  }, []);

  return (
    <div>
      <h1>Here is your post!</h1>
      <hr/>
      <p>{textBlock}</p>
      

    </div>
  );
}

export default Post;
