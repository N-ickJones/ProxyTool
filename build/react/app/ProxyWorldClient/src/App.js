import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { Diagrams } from './components/Diagrams';
//import AuthorizeRoute from './components/api-authorization/AuthorizeRoute';
//import ApiAuthorizationRoutes from './components/api-authorization/ApiAuthorizationRoutes';
//import { ApplicationPaths } from './components/api-authorization/ApiAuthorizationConstants';

import './custom.css'

export default class App extends Component {
  static displayName = App.name;
  //<Route path={ApplicationPaths.ApiAuthorizationPrefix} component={ApiAuthorizationRoutes} />
  render () {
    return (
      <Layout>
        <Route exact path='/' component={Home} />
        <Route path='/diagrams' component={Diagrams} />
        
      </Layout>
    );
  }
}
