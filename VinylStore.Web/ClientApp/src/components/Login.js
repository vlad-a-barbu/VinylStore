import React, { Component } from 'react';

export class Login extends Component {
    static displayName = Login.name;

    constructor(props) {
        super(props);
        this.state = { email: '', password: '' };
        this.login = this.login.bind(this);
        this.handleChange = this.handleChange.bind(this);
    }

    async login() {
        console.log(this.state);
    }

    handleChange(e) {
        const { name, value } = e.target;
        this.setState({ [name]: value });
    }

    render() {
        const { email, password } = this.state;
        return (
            <div>
                <h1>Login</h1>
                <br />

                <label for="email"><b>Email</b></label><br/>
                <input type="email" placeholder="Enter your email" name="email" value={email} onChange={this.handleChange} /><br /><br />

                <label for="password"><b>Password</b></label><br />
                <input type="password" placeholder="Enter your password" name="password" value={password} onChange={this.handleChange} /><br /><br />

                <button className="btn btn-primary" onClick={this.login}>Login</button>
            </div>
        );
    }
}
