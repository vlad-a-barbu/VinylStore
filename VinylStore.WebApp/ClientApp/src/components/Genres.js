import React, { Component } from 'react';
import GenreModal from './GenreModal';
import EditGenreModal from './EditGenreModal';

export class Genres extends Component {
    static displayName = Genres.name;

    constructor(props) {
        super(props);
        this.state = { models: [], loading: true };
        this.handleDelete = this.handleDelete.bind(this);
        this.handleEdit = this.handleEdit.bind(this);
        this.renderTable = this.renderTable.bind(this);
        this.addModelHandler = this.addModelHandler.bind(this);
    }

    componentDidMount() {
        this.populate();
    }

    async handleDelete(id) {

        console.log('deleting ' + id);

        await fetch(
            'genres/delete',
            {
                method: "DELETE",
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer ' + localStorage.getItem("jwt")
                },
                body: JSON.stringify(id)
            }
        ).then(response => {
            if (response.status === 401){
                alert("Unauthorized user");
            }
            else if (response.ok) {
                this.setState(
                    {
                        models: this.state.models.filter(m => m["id"] !== id),
                        loading: false
                    }
                );
            }
            else{
                alert("Something went wrong");
            }
        });
    }

    async handleEdit(id) {
        console.log('editing ' + id);

    }

    renderTable(models) {
        return (
            <>
                <table className='table table-striped' aria-labelledby="tabelLabel">
                    <thead>
                        <tr>
                            <th>Id</th>
                            <th>Name</th>
                            <th></th>
                        </tr>
                    </thead>
                    <tbody>
                        {models.map(model =>
                            <tr key={model.id}>
                                <td>{model.id}</td>
                                <td>{model.name}</td>
                                <td>

                                    <EditGenreModal handler={this.updateModelHandler} model={model} />

                                    <button onClick={() => this.handleDelete(model.id)}>
                                        <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" className="bi bi-trash-fill" viewBox="0 0 16 16">
                                            <path d="M2.5 1a1 1 0 0 0-1 1v1a1 1 0 0 0 1 1H3v9a2 2 0 0 0 2 2h6a2 2 0 0 0 2-2V4h.5a1 1 0 0 0 1-1V2a1 1 0 0 0-1-1H10a1 1 0 0 0-1-1H7a1 1 0 0 0-1 1H2.5zm3 4a.5.5 0 0 1 .5.5v7a.5.5 0 0 1-1 0v-7a.5.5 0 0 1 .5-.5zM8 5a.5.5 0 0 1 .5.5v7a.5.5 0 0 1-1 0v-7A.5.5 0 0 1 8 5zm3 .5v7a.5.5 0 0 1-1 0v-7a.5.5 0 0 1 1 0z" />
                                        </svg>
                                    </button>

                                </td>
                            </tr>
                        )}
                    </tbody>
                </table>
            </>
        );
    }

    addModelHandler = (model) => {
        this.setState({
            models: this.state.models.concat(model),
            loading: false
        });
    }

    updateModelHandler = (model) => {
        const existing = this.state.models.find(m => m.id == model.id);
        const existingItemId = this.state.models.indexOf(existing);
        this.state.models[existingItemId] = model;
        this.setState({
            models: this.state.models,
            loading: false
        });
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : this.renderTable(this.state.models);

        return ( 
            <div> 
                <h1 id="tabelLabel" >Genres</h1>
                <GenreModal handler={this.addModelHandler}/>
                {contents}
            </div>
        );
    }

    async populate() {
        const response = await fetch('genres/getall');
        const data = await response.json();
        this.setState({ models: data, loading: false });
    }
}
