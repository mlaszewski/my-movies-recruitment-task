import axios from "axios";

export async function getMovieList() {
    const response = await axios.get(`/api/Movie`);
    return response;
}

export async function addMovie(movie) {
    const response = await axios.post('/api/Movie', movie);
    return response;
}

export async function editMovie(id, newMovie) {
    const response = await axios.put(`/api/Movie/${id}`, newMovie);
    return response;
}

export async function deleteMovie(id) {
    const response = await axios.delete(`/api/Movie/${id}`);
    return response;
}

export async function getExternalMovieList() {
    const response = await axios.get("/MyMovies");
    return response;
}