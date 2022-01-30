import { React, useState } from 'react';
import { Button, Modal } from 'react-bootstrap';

function AlbumModal(props) {
    const [show, setShow] = useState(false);
    const [name, setName] = useState("");
	const [releaseDate, setReleaseDate] = useState("");
    const [artist, setArtist] = useState("");
    const push = props.handler;

    const handleClose = () => {
        setShow(false);
    }

    const handleNameChange = (event) => {
        setName(event.target.value);
    }

	const handleReleaseDateChange = (event) => {
        setReleaseDate(event.target.value);
    }

	const handleArtistChange = (event) => {
        setArtist(event.target.value);
    }

    const handleSave = async () => {

        const data = {
            name,
			releaseDate,
			artist
        };

        console.log(data);

        await fetch(
            'albums/create',
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
            else if (response.ok){
                response.json().then(id => {
                    data["id"] = id;
                    console.log(data);
                    push(data);
                    setName("");
					setReleaseDate("");
					setArtist("");
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
                    <Modal.Title>Album Designer</Modal.Title>
                </Modal.Header>
                <Modal.Body>

                    <label htmlFor="name"><b>Name</b></label><br />
                    <input type="text" placeholder="Album name" name="name" defaultValue={name} onChange={handleNameChange}/><br />
                
					<label htmlFor="releaseDate"><b>Release date</b></label><br />
                    <input type="date" placeholder="Release date" name="releaseDate" defaultValue={releaseDate} onChange={handleReleaseDateChange}/><br />

					<label htmlFor="artist"><b>Artist</b></label><br />
                    <input type="text" placeholder="Artist" name="artist" defaultValue={artist} onChange={handleArtistChange}/><br />

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

export default AlbumModal;
