import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { Login } from './components/Login';
import { Genres } from './components/Genres';
import { Users } from './components/Users';
import { Albums } from './components/Albums';
import { Artists } from './components/Artists';

import './custom.css'

export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
      <Layout>
        <Route exact path='/' component={Home} />
        <Route path='/login' component={Login} />
        <Route path='/genres' component={Genres} />
        <Route path='/users' component={Users} />
        <Route path='/albums' component={Albums} />
        <Route path='/artists' component={Artists} />
      </Layout>
    );
  }
}
