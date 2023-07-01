import React, { useEffect, useState } from 'react';
import tokenHandler from '../types/handlers';
import UserTextBlockItem from './UserTextBlockItem';

function UserTextBlockList() {
  const [textBlocks, setTextBlocks] = useState<ITextBlock[]>([]);
  const [isEmpty, setIsEmpty] = useState<boolean>(false);
  const tokenHandle = new tokenHandler();
  useEffect(() => {
    async function getUserName(): Promise<void> {
      try {
        await fetch('user/postlist', {
          method: 'GET',
          headers: {
            'Content-Type': 'application/json;charset=utf-8',
            ...tokenHandle.getAuthorization(),
          }
        }).then((response) => response.json())
        .then((data) => {
          if(data.length == 0) {
            setIsEmpty(true);
          }
          setTextBlocks(data);
        });
      } catch (error) {
        console.log(error);
      }
      }

      getUserName();
  }, [])


  return (
    <div>
      <h1>Your posts:</h1>
      {isEmpty ? <p>No posts found</p> : <></>}
      {
        textBlocks.map((block, index) =>
        (
          <UserTextBlockItem {...block} key={index} />
        ))
      }
    </div>
  );
}

export default UserTextBlockList;

