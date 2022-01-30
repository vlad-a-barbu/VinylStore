import { React, useState } from 'react';
import { Button, Modal } from 'react-bootstrap';

function ArtistModal(props) {
    const [show, setShow] = useState(false);
    const [name, setName] = useState("");
    const push = props.handler;

    const handleClose = () => {
        const data = {
            name
        };

        console.log(data);
        setShow(false);
    }

    const handleNameChange = (event) => {
        setName(event.target.value);
    }

    const handleSave = async () => {

        const data = {
            name
        };

        console.log(data);

        await fetch(
            'artists/create',
            {
                method: "POST",
                headers: {
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
            else if (response.ok) {
                response.json().then(id => {
                    data["id"] = id;
                    console.log(data);
                    push(data);
                    setName("");
                    setShow(false);
                });
            } else {
				alert("Something went wrong");
			}
        });
    }
    const handleShow = () => setShow(true);

    return (
        <>
            <Button variant="primary" onClick={handleShow}>
                Create
            </Button>

            <Modal show={show} onHide={handleClose}>
                <Modal.Header closeButton>
                    <Modal.Title>Artist Designer</Modal.Title>
                </Modal.Header>
                <Modal.Body>

                    <label htmlFor="name"><b>Name</b></label><br />
                    <input type="text" placeholder="Artist name" name="name" defaultValue={name} onChange={handleNameChange}/><br />
                
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

export default ArtistModal;
