import { Box, Button, Card } from '@mui/material'
import { useNavigate } from 'react-router-dom'

const Home = () => {
  const navigate = useNavigate();

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
        <Box>
          <Button
            onClick={() => navigate('/todo')}
          >
            go to todo page
          </Button>
        </Box>
      </Card>
    </Box>
  )
}

export default Home