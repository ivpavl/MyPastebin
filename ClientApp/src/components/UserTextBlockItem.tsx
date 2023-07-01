import React from 'react';
import '../styles/UserTextBlockItem.css';


function UserTextBlockItem(block: ITextBlock) {
  return (
    <div className={"textBlockElement"}>
        <h5>{block.title}</h5>
        <p>{block.text}</p>
        <a href={"post?id=" + block.hashId}>Hash ID: {block.hashId}</a>
    </div>
  );
}

export default UserTextBlockItem;

