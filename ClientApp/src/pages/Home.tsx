import React from 'react';
import TextBlockForm from '../components/TextBlockForm';
import UserStatus from '../components/UserStatus';


function Home() {
  return (
    <div>
      
      <h3>Welcome to MyPastebin!</h3>
      <UserStatus/>
      <TextBlockForm/>

    </div>
  );
}

export default Home;
