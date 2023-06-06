import React from 'react'

const TodoDate = ({ date }) => {
    return (
        <p>Created on: {new Date(date).toLocaleString('en-US')}</p>
    );
}

export default TodoDate