import { React, useState } from 'react';
import { Button, Modal } from 'react-bootstrap';

function EditUserModal(props) {
    const [show, setShow] = useState(false);
	const [user, setUser] = useState(props.model);
    const updateHandler = props.handler;

    const handleClose = () => {
        setShow(false);
    }

    const handleSave = async () => {

        await fetch(
            'users/update',
            {
                method: "PUT",
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer ' + localStorage.getItem("jwt")
                },
                body: JSON.stringify(user)
            }
        ).then(response => {
            if (response.status === 401){
                alert("Unauthorized user");
            } else if (response.ok) {
                updateHandler(user);
                setShow(false);
            } else {
                alert("Something went wrong");
            }
        });
    }

	const handleFirstNameChange = (event) => { user["firstName"] = event.target.value; setUser(user); }
    const handleLastNameChange = (event) => { user["lastName"] = event.target.value; setUser(user); }
    const handleEmailChange = (event) => { user["email"] = event.target.value; setUser(user); }
    const handleCountryChange = (event) => { user["country"] = event.target.value; setUser(user); }
    const handleCityChange = (event) => { user["city"] = event.target.value; setUser(user); }
    const handleAddressLine1Change = (event) => { user["addressLine1"] = event.target.value; setUser(user); }
    const handleAddressLine2Change = (event) => { user["addressLine2"] = event.target.value; setUser(user); }

    const handleShow = () => setShow(true);

    return (
        <>
            <button onClick={handleShow}>
                <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" className="bi bi-pencil-square" viewBox="0 0 16 16">
                    <path d="M15.502 1.94a.5.5 0 0 1 0 .706L14.459 3.69l-2-2L13.502.646a.5.5 0 0 1 .707 0l1.293 1.293zm-1.75 2.456-2-2L4.939 9.21a.5.5 0 0 0-.121.196l-.805 2.414a.25.25 0 0 0 .316.316l2.414-.805a.5.5 0 0 0 .196-.12l6.813-6.814z" />
                    <path fillRule="evenodd" d="M1 13.5A1.5 1.5 0 0 0 2.5 15h11a1.5 1.5 0 0 0 1.5-1.5v-6a.5.5 0 0 0-1 0v6a.5.5 0 0 1-.5.5h-11a.5.5 0 0 1-.5-.5v-11a.5.5 0 0 1 .5-.5H9a.5.5 0 0 0 0-1H2.5A1.5 1.5 0 0 0 1 2.5v11z" />
                </svg>
            </button>


            <Modal show={show} onHide={handleClose}>
                <Modal.Header closeButton>
                    <Modal.Title>User Designer</Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    
				< label htmlFor="firstName" >< b > First name </ b ></ label >< br />
                    < input type="text" placeholder="First name" name="firstName" defaultValue={user.firstName}
                        onChange={handleFirstNameChange} />< br />

                    < label htmlFor="lastName" >< b > Last name </ b ></ label >< br />
                    < input type="text" placeholder="Last name" name="lastName" defaultValue={user.lastName}
                        onChange={handleLastNameChange} />< br />

                    < label htmlFor="email" >< b > Email </ b ></ label >< br />
                    < input type="email" placeholder="Email" name="email" defaultValue={user.email}
                        onChange={handleEmailChange} />< br />

                    < label htmlFor="country" >< b > Country </ b ></ label >< br />
                    < input type="text" placeholder="Country" name="country" defaultValue={user.country}
                        onChange={handleCountryChange} />< br />

                    < label htmlFor="city" >< b > City </ b ></ label >< br />
                    < input type="text" placeholder="City" name="city" defaultValue={user.city}
                        onChange={handleCityChange} />< br />

                    < label htmlFor="addressLine1" >< b > Address line 1 </ b ></ label >< br />
                    < input type="text" placeholder="Address line 1" name="addressLine1" defaultValue={user.addressLine1}
                        onChange={handleAddressLine1Change} />< br />

                    < label htmlFor="addressLine2" >< b > Address line 2 </ b ></ label >< br />
                    < input type="text" placeholder="Address line 2" name="addressLine2" defaultValue={user.addressLine2}
                        onChange={handleAddressLine2Change} />< br />

                </Modal.Body>
                <Modal.Footer>
                    <Button variant="secondary" onClick={handleClose}>
                        Close
                    </Button>
                    <Button variant="primary" onClick={handleSave}>
                        Save Changes
                    </Button>
                </Modal.Footer>
            </Modal>
        </>
    );
}

export default EditUserModal;
