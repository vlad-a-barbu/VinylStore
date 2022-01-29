import { React, useState } from 'react';
import { Button, Modal } from 'react-bootstrap';

function AdminModal(props) {
    const [show, setShow] = useState(false);
    const [firstName, setFirstName] = useState("");
    const [lastName, setLastName] = useState("");
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const [confirmPassword, setConfirmPassword] = useState("");
    const [country, setCountry] = useState("");
    const [city, setCity] = useState("");
    const [addressLine1, setAddressLine1] = useState("");
    const [addressLine2, setAddressLine2] = useState("");

    const push = props.handler;

    const handleClose = () => {
        setShow(false);
    }

    const handleFirstNameChange = (event) => setFirstName(event.target.value);
    const handleLastNameChange = (event) => setLastName(event.target.value);
    const handleEmailChange = (event) => setEmail(event.target.value);
    const handlePasswordChange = (event) => setPassword(event.target.value);
    const handleConfirmPasswordChange = (event) => setConfirmPassword(event.target.value);
    const handleCountryChange = (event) => setCountry(event.target.value);
    const handleCityChange = (event) => setCity(event.target.value);
    const handleAddressLine1Change = (event) => setAddressLine1(event.target.value);
    const handleAddressLine2Change = (event) => setAddressLine2(event.target.value);

    const handleSave = async () => {

        const data = {
            firstName,
            lastName,
            email,
            password,
            confirmPassword,
            country,
            city,
            addressLine1,
            addressLine2
        };

        console.log(data);

        const response = await fetch(
            'authentication/registerAdmin',
            {
                method: "POST",
                headers:
                {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer ' + localStorage.getItem("jwt")
                },
                body: JSON.stringify(data)
            }
        ).then((response) => {
            if (response.status === 401) {
                alert("Unauthorized user");
            }
            else {
                response.json().then(id => {
                    data["id"] = id;
                    data["role"] = 1;
                    console.log(data);
                    push(data);
                    setShow(false);
                });
            }
        });
    }
    const handleShow = () => setShow(true);

    return (

        <>

            < Button style = {{margin: '5px'}} variant="primary" onClick={handleShow}>
                Create Admin
            </ Button >


            < Modal show={show}
                onHide={handleClose}>

                < Modal.Header closeButton >

                    < Modal.Title > Admin Designer </ Modal.Title >

                </ Modal.Header >

                < Modal.Body >


                    < label htmlFor="firstName" >< b > First name </ b ></ label >< br />
                    < input type="text" placeholder="First name" name="firstName" defaultValue={firstName}
                        onChange={handleFirstNameChange} />< br />

                    < label htmlFor="lastName" >< b > Last name </ b ></ label >< br />
                    < input type="text" placeholder="Last name" name="lastName" defaultValue={lastName}
                        onChange={handleLastNameChange} />< br />

                    < label htmlFor="email" >< b > Email </ b ></ label >< br />
                    < input type="email" placeholder="Email" name="email" defaultValue={email}
                        onChange={handleEmailChange} />< br />

                    < label htmlFor="password" >< b > Password </ b ></ label >< br />
                    < input type="password" placeholder="Password" name="password" defaultValue={password}
                        onChange={handlePasswordChange} />< br />

                    < label htmlFor="confirmPassword" >< b > Confirm Password </ b ></ label >< br />
                    < input type="password" placeholder="Confirm Password" name="confirmPassword" defaultValue={confirmPassword}
                        onChange={handleConfirmPasswordChange} />< br />

                    < label htmlFor="country" >< b > Country </ b ></ label >< br />
                    < input type="text" placeholder="Country" name="country" defaultValue={country}
                        onChange={handleCountryChange} />< br />

                    < label htmlFor="city" >< b > City </ b ></ label >< br />
                    < input type="text" placeholder="City" name="city" defaultValue={city}
                        onChange={handleCityChange} />< br />

                    < label htmlFor="addressLine1" >< b > Address line 1 </ b ></ label >< br />
                    < input type="text" placeholder="Address line 1" name="addressLine1" defaultValue={addressLine1}
                        onChange={handleAddressLine1Change} />< br />

                    < label htmlFor="addressLine2" >< b > Address line 2 </ b ></ label >< br />
                    < input type="text" placeholder="Address line 2" name="addressLine2" defaultValue={addressLine2}
                        onChange={handleAddressLine2Change} />< br />


                </ Modal.Body >

                < Modal.Footer >

                    < Button variant="secondary" onClick={handleClose}>
                        Close
                    </ Button >

                    < Button variant="primary" onClick={handleSave}>
                        Save Changes
                    </ Button >

                </ Modal.Footer >

            </ Modal >

        </>
    );
}

export default AdminModal;
