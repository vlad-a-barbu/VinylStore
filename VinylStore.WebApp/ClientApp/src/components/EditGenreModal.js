﻿import { React, useState } from 'react';
import { Button, Modal } from 'react-bootstrap';

function EditGenreModal(props) {
    const [show, setShow] = useState(false);
    const [genre, setGenre] = useState(props.model);
    const updateHandler = props.handler;

    const handleClose = () => {
        setShow(false);
    }

    const handleSave = async () => {

        console.log(genre);

        const response = await fetch(
            'genres/update',
            {
                method: "PUT",
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer ' + localStorage.getItem("jwt")
                },
                body: JSON.stringify(genre)
            }
        );

        await response.text().then(() => {
            updateHandler(genre);
            setShow(false);
        });
    }

    const handleNameChange = (event) => {
        genre["name"] = event.target.value;
        setGenre(genre);
    }

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
                    <Modal.Title>Genre Designer</Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    
                    <label htmlFor="name"><b>Name</b></label><br />
                    <input type="text" placeholder="Genre name" name="name" defaultValue={genre.name} onChange={handleNameChange} /><br />

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

export default EditGenreModal;
