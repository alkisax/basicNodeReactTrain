import { Box, Button, Card, TextField } from "@mui/material"

interface Props {
handleUsernameSubmit: (e: React.SyntheticEvent) => void;
usernameInput: string;
setUsernameInput: React.Dispatch<React.SetStateAction<string>>;
}

const Login = ({ handleUsernameSubmit, usernameInput, setUsernameInput }: Props) => {
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

export default Login