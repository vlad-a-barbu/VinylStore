import React, { Component } from 'react';

export class Home extends Component {
  static displayName = Home.name;

  render () {
    return (
      <div style={{textAlign: 'center'}}>
            <h1>Demo API + basic UI for CRUD operations</h1>
            <br/>
        <h5>You can make use of this <a href='https://github.com/vlad-a-barbu/VinylStore'>project</a> for future architecture and design decisions</h5>
      </div>
    );
  }
}
