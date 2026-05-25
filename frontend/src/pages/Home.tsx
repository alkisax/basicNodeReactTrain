import { Box, Button, Card } from '@mui/material'
import { useNavigate } from 'react-router-dom'

const Home = () => {
  const navigate = useNavigate();

  return (
    <Box
      sx={{
        display: 'flex',
        flexDirection: 'column',
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
        <Box>
          <Button
            onClick={() => navigate('/todo')}
          >
            go to todo page
          </Button>
        </Box>
      </Card>

      <Button
        onClick={() => navigate('/bootstrap-home')}
        sx={{
          fontSize: '0.7rem',
          padding: '2px 8px',
          minWidth: 'unset'
        }}
      >
        go to bootstrap mirror
      </Button>
    </Box>
  )
}

export default Home