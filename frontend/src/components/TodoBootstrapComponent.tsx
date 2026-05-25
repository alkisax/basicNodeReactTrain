// frontend\src\components\TodoBootstrapComponent.tsx
import { useEffect, useState } from "react"
import { backendUrl } from '../constants/constants'
import axios from "axios"
import LoginBootstrap from "./LoginBootstrap"

type Todo = {
  _id: string
  todo: string;
  user: string;
  completed: boolean;
  createdAt?: Date;
  updatedAt?: Date;
}

const TodoBootstrapComponent = () => {
  const [todos, setTodos] = useState<Todo[]>([])
  const [user, setUser] = useState<string | null>(null)
  const [usernameInput, setUsernameInput] = useState('')
  const [todoInput, setTodoInput] = useState('')
  const [editingTodoId, setEditingTodoId] = useState<string | null>('')
  const [editingText, setEditingText] = useState('')

  const handleUsernameSubmit = (e: React.SyntheticEvent) => {
    e.preventDefault()
    console.log("user submit", usernameInput);
    setUser(usernameInput)
  }

  useEffect(() => {

    const fetchTodos = async () => {
      if (user) {
        const res = await axios.get(
          `${backendUrl}/todo/user-todos/${user}`
        )
        console.log("res: ", res);
        // setUser(res.data.data[0].user)
        setTodos(res.data.data)
      }
    }
    fetchTodos()
  }, [user])

  const handleCreateTodo = async (todo: string) => {
    console.log(`user: ${user}, todo: ${todo}`);

    const createdRes = await axios.post(
      `${backendUrl}/todo/create-todo`, {
      todo: todo,
      user: user
    }
    )
    console.log("createdRes: ", createdRes);
    setTodos((prev) => [...prev, createdRes.data.data])
  }

  const handleDeleteTodoById = async (id: string) => {
    const confirmed = confirm(`are you sure you want to delete todo - id: ${id}?`)
    if (!confirmed) return
    const deleteRes = await axios.post(
      `${backendUrl}/todo/delete-todo/${id}`
    )
    console.log("deleteRes", deleteRes);
    setTodos((prev) => prev.filter((t) => t._id !== id))
  }

  const handleEditTodoTextById = async (id: string, todo: string) => {
    const editRes = await axios.post(
      `${backendUrl}/todo/update-todo/${id}`, {
      todo: todo
    })
    setTodos((prev) =>
      prev.map((t) => t._id === id ? editRes.data.data : t)
    )
    console.log("editRes: ", editRes)
    setEditingText('')
    setEditingTodoId('')
  }

  if (!user) {
    return (
      <LoginBootstrap
        handleUsernameSubmit={handleUsernameSubmit}
        usernameInput={usernameInput}
        setUsernameInput={setUsernameInput}
      />
    )
  }

  return (
    <>
      <div
        className='
        d-flex
        justify-content-center
        align-items-center
        min-vh-100
      '
      >
        <div>

          <h3 className='mb-3 text-light'>
            {user} todos
          </h3>

          <h5 className='mb-2 text-light'>
            Add new "todo" →
          </h5>

          <div className='d-flex align-items-center mb-3 gap-2'>
            <input
              value={todoInput}
              onChange={(e) => setTodoInput(e.target.value)}
              type='text'
              placeholder='Todo'
              className='form-control bg-dark text-light border-secondary'
            />

            <button
              onClick={() => handleCreateTodo(todoInput)}
              className='btn btn-success'
            >
              ✔️
            </button>
          </div>

          <div className='table-responsive'>
            <table className='table table-dark table-striped table-bordered align-middle'>

              <thead>
                <tr>
                  <th>Todos</th>
                  <th>UpdatedAt</th>
                  <th>Actions</th>
                </tr>
              </thead>

              <tbody>
                {todos.map((t, i) => (
                  <tr key={i}>

                    <td>
                      {editingTodoId !== t._id &&
                        t.todo
                      }

                      {editingTodoId === t._id &&
                        <div className='d-flex gap-2'>
                          <input
                            value={editingText}
                            onChange={(e) => setEditingText(e.target.value)}
                            type='text'
                            className='form-control'
                          />

                          <button
                            onClick={() =>
                              handleEditTodoTextById(
                                t._id,
                                editingText
                              )
                            }
                            className='btn btn-success'
                          >
                            ✔️
                          </button>
                        </div>
                      }
                    </td>

                    <td>
                      {
                        t.updatedAt
                          ? new Date(t.updatedAt).toLocaleString('el-GR', {
                            hour: '2-digit',
                            minute: '2-digit',
                            year: 'numeric',
                            month: 'long',
                            day: 'numeric'
                          })
                          : ''
                      }
                    </td>

                    <td>
                      <div className='d-flex gap-2'>

                        <button
                          className='btn btn-warning btn-sm'
                          onClick={() => {
                            setEditingTodoId(t._id)
                            setEditingText(t.todo)
                          }}
                        >
                          Edit
                        </button>

                        <button
                          className='btn btn-danger btn-sm'
                          onClick={() => handleDeleteTodoById(t._id)}
                        >
                          Delete
                        </button>

                      </div>
                    </td>

                  </tr>
                ))}
              </tbody>

            </table>
          </div>

          <button
            onClick={() => setUser(null)}
            className='btn btn-secondary mt-3'
          >
            Logout
          </button>

        </div>
      </div>
    </>
  )
}

export default TodoBootstrapComponent