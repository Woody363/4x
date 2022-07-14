import React, { Component } from 'react';

export class Testing extends Component {
  static displayName = Testing.name;

  
  constructor(props) {
    super(props);
    this.state = { currentCount: 0 };
    this.incrementCounter = this.incrementCounter.bind(this);
    this.state = { forecasts: [], loading: true };
  }

  incrementCounter() {
    this.setState({
      currentCount: this.state.currentCount + 1
    });
  }

  componentDidMount() {
    this.populateWeatherData();
  }
  
  render() {
    console.log(this.state.loading);
    console.log(this.state.forecasts);
    return (
      <div>
        <h1>Counter</h1>
        <p>Testing adding a new page2</p>  {/* also need to add bits to AppRoutes.js and NavMenu.js when creating new page*/}
        
        <p>This is a simple example of a React component.</p>
        

        <p aria-live="polite">Current count: <strong>{this.state.currentCount}</strong></p>

        <button className="btn btn-primary" onClick={this.incrementCounter}>Increment</button>
      </div>
    );
  }


async populateWeatherData() {
  const response = await fetch('weatherforecast');
  const data = await response.json();
  this.setState({ forecasts: data, loading: false });
}
}