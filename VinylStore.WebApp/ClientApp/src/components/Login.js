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

        const loginViewModel = {
            Email: this.state.email,
            Password: this.state.password
        }

        const response = await fetch(
            'authentication/authenticate',
            {
                method: "POST",
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(loginViewModel)
            }
        );

        const token = await response.json();
        console.log(token);
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

                <label htmlFor="email"><b>Email</b></label><br/>
                <input type="email" placeholder="Enter your email" name="email" value={email} onChange={this.handleChange} /><br /><br />

                <label htmlFor="password"><b>Password</b></label><br />
                <input type="password" placeholder="Enter your password" name="password" value={password} onChange={this.handleChange} /><br /><br />

                <button className="btn btn-primary" onClick={this.login}>Login</button>
            </div>
        );
    }
}
