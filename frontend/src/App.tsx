import { useEffect, useState } from "react"
import { backendUrl } from './constants/constants'
import axios from "axios"
import { Paper, Box, Card, TableBody, TableCell, TableContainer, TableHead, TableRow, Typography, Table, TextField, Button, Tooltip, IconButton } from "@mui/material"
import DeleteIcon from '@mui/icons-material/Delete'
import EditIcon from '@mui/icons-material/Edit'

type Todo = {
  _id: string
  todo: string;
  user: string;
  completed: boolean;
  createdAt?: Date;
  updatedAt?: Date;
}

const App = () => {
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
      <Box
        sx={{
          display: 'flex',
          justifyContent: 'center',
          alignItems: 'center',
          minHeight: '90vh'
        }}
      >
        <Card
          sx={{
            width: 300,
            height: 200,
            color: '#fff',
            bgcolor: '#1e1e1e',
            borderRadius: 4,
            justifyContent: 'center',
            alignItems: 'center',
            display: 'flex',
          }}
        >
          <Box
            component='form'
            onSubmit={handleUsernameSubmit}
          >
            <TextField
              value={usernameInput}
              onChange={(e) => setUsernameInput(e.target.value)}
              label='Username'
              variant='outlined'
              size='small'
              sx={{
                input: { color: 'white' },
                label: { color: 'gray' }
              }}
            />
            <Button type='submit'>
              ✔️
            </Button>
          </Box>
        </Card>
      </Box>
    )
  }

  return (
    <>
      <Box
        sx={{
          display: 'flex',
          justifyContent: 'center',
          alignItems: 'center',
          minHeight: '90vh'
        }}
      >
        <Box
          sx={{
            flexDirection: 'row',
            justifyContent: 'center',
            alignItems: 'center',
          }}
        >

          <Typography variant="h5" sx={{ mb: 3 }}>
            {user} todos
          </Typography>

          <Typography variant="h6" sx={{ mb: 2 }}>
            Add new "todo"→
          </Typography>
          <TextField
            value={todoInput}
            onChange={(e) => setTodoInput(e.target.value)}
            label='Todo'
            variant='outlined'
            size='small'
            sx={{
              '& fieldset': {
                borderColor: 'white',
              },
              input: { color: 'white' },
              label: { color: 'gray' },
              mb: 2
            }}
          />
          <Button
            type='submit'
            onClick={() => handleCreateTodo(todoInput)}
          >
            ✔️
          </Button>


          <TableContainer component={Paper}>
            <Table>
              <TableHead>
                <TableRow>
                  <TableCell>Todos</TableCell>
                  <TableCell>UpdatedAt</TableCell>
                  <TableCell>Actions</TableCell>
                </TableRow>
              </TableHead>

              <TableBody>
                {todos.map((t, i) => (
                  <TableRow key={i}>
                    <TableCell>
                      {editingTodoId !== t._id &&
                        t.todo
                      }
                      {editingTodoId === t._id &&
                        <>
                          <TextField
                            value={editingText}
                            onChange={(e) => setEditingText(e.target.value)}
                            size='small'
                            sx={{
                              input: { color: 'black' },
                              label: { color: 'gray' },
                            }}
                          />
                          <Button
                            type='submit'
                            onClick={() => handleEditTodoTextById(t._id, editingText)}
                          >
                            ✔️
                          </Button>
                        </>
                      }
                    </TableCell>
                    <TableCell>
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
                    </TableCell>
                    <TableCell>
                      <Tooltip title='edit todo'>
                        <IconButton
                          onClick={() => {
                            setEditingTodoId(t._id)
                            setEditingText(t.todo)
                          }}
                        >
                          <EditIcon />
                        </IconButton>
                      </Tooltip>
                      <Tooltip title='Delete todo'>
                        <IconButton
                          onClick={() => handleDeleteTodoById(t._id)}
                        >
                          <DeleteIcon />
                        </IconButton>
                      </Tooltip>
                    </TableCell>
                  </TableRow>
                ))}
              </TableBody>

            </Table>
          </TableContainer>

          <Button onClick={() => setUser(null)}>
            Logout
          </Button>
        </Box>
      </Box>
    </>


  )
}

export default App