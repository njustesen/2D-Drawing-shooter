using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading;

//Server class
public class Server : MonoBehaviour {

	//server stuff
	private TcpListener tcpListener = new TcpListener(IPAddress.Any, 3000);
	private Thread listenThread;
	private List <TcpClient> clients = new List<TcpClient>();
	public GUIText wiiInput= new GUIText();
	public string fullmessage;
	public bool clientConnected = false;
	public bool inputRead = false;
	private TcpClient theClient;
	private NetworkStream clientStream;
	//////////////////
	//game variables//
	/////////////////
	public int numberOfPlayers;
	//player1
	public int player1XSpeed = 0;
	public bool player1Jump = false;
	public bool player1Shoot = false;
	public bool player1Draw = false;
	public int player1AimX = 0;
	public int player1AimY = 0;
	//player2
	public int player2XSpeed = 0;
	public bool player2Jump = false;
	public bool player2Shoot = false;
	public bool player2Draw = false;
	public int player2AimX = 0;
	public int player2AimY = 0;
	//player3
	public int player3XSpeed = 0;
	public bool player3Jump = false;
	public bool player3Shoot = false;
	public bool player3Draw = false;
	public int player3AimX = 0;
	public int player3AimY = 0;
	//player4
	public int player4XSpeed = 0;
	public bool player4Jump = false;
	public bool player4Shoot = false;
	public bool player4Draw = false;
	public int player4AimX = 0;
	public int player4AimY = 0;
	
	// Use this for initialization
	void Start () {
		Debug.Log("start");
	//	this.tcpListener = 
	//	this.listenThread = new Thread(new ThreadStart(ListenForClients));
	//	this.listenThread.Start();
		this.tcpListener.Start();
		ListenForClients();
		clientStream  = theClient.GetStream();		
	}
	
	// Update is called once per frame
	void Update () {
		if(clientConnected == true){
			wiiInput.text = HandleClientComm();
			SplitString(HandleClientComm());
		}else{wiiInput.text ="NO INPUT";}
	}
	
	private void ListenForClients() {
		Debug.Log("listenforclients");
            
				Debug.Log("while true");
                //blocks until a client has connected to the server
                theClient = this.tcpListener.AcceptTcpClient();
				clientConnected = true;
				Debug.Log("client added");
				
                //create a thread to handle communication with connected client
               
            
        }

        private string HandleClientComm() {
			
			Debug.Log("HandleClientComm");
            fullmessage = "";
            
                int bytesRead = 0;
				byte[] message = new byte[4096];
                try
                {
                    //blocks until a client sends a message
					 Debug.Log("blocked");
                    bytesRead = clientStream.Read(message, 0, 4096);
                }catch{
                    //a socket error has occurred
                    Debug.Log("socketerror");
                }

                if(bytesRead == 0){
                    Debug.Log("bytesRead = 0");
                }

                //message has successfully been recieved
                ASCIIEncoding encoder = new ASCIIEncoding();
                //System.Console.WriteLine(encoder.GetString(message, 0, bytesRead));
				fullmessage = encoder.GetString(message, 0, bytesRead);
            
			
            return fullmessage;
        }
		
		public void SplitString(string message){
			
			player1XSpeed = 0;
			player1Jump = false;
			player1Shoot = false;
			player1Draw = false;
			player1AimX = 0;
			player1AimY = 0;
			player2XSpeed = 0;
			player2Jump = false;
			player2Shoot = false;
			player2Draw = false;
			player2AimX = 0;
			player2AimY = 0;
			player3XSpeed = 0;
			player3Jump = false;
			player3Shoot = false;
			player3Draw = false;
			player3AimX = 0;
			player3AimY = 0;
			player4XSpeed = 0;
			player4Jump = false;
			player4Shoot = false;
			player4Draw = false;
			player4AimX = 0;
			player4AimY = 0;
			
			string [] wiiCommands = message.Split('/'); 
		
				for(int i = 0; i < wiiCommands.Length; i++){
					if(wiiCommands[i] == "p1A"){
						player1Draw=true;
					}else if(wiiCommands[i] == "p1B"){
						player1Shoot=true;
					}else if(wiiCommands[i] == "p1Z"){
						player1Jump=true;
					}else if(wiiCommands[i] == "p1NJ"){
						player1XSpeed = Convert.ToInt16(wiiCommands[i+1]);
					}else if(wiiCommands[i] == "p1IRX"){
						player1AimX = Convert.ToInt16(wiiCommands[i+1]);
					}else if(wiiCommands[i] == "p1IRY"){
						player1AimY = Convert.ToInt16(wiiCommands[i+1]);
					}else 
					
					if(wiiCommands[i] == "p2A"){
						Debug.Log("P2A");
						player2Draw=true;
					}else if(wiiCommands[i] == "p2B"){
						player2Shoot=true;
					}else if(wiiCommands[i] == "p2Z"){
						player2Jump=true;
					}else if(wiiCommands[i] == "p2NJ"){
						Debug.Log("P2NJ");
						player2XSpeed = Convert.ToInt16(wiiCommands[i+1]);
						Debug.Log("p2XSPEED = "+player2XSpeed);
					}else if(wiiCommands[i] == "p2IRX"){
						player2AimX = Convert.ToInt16(wiiCommands[i+1]);
					}else if(wiiCommands[i] == "p2IRY"){
						player2AimY = Convert.ToInt16(wiiCommands[i+1]);
					}else 
					
					if(wiiCommands[i] == "p3A"){
						player3Draw=true;
					}else if(wiiCommands[i] == "p3B"){
						player3Shoot=true;
					}else if(wiiCommands[i] == "p3Z"){
						player3Jump=true;
					}else if(wiiCommands[i] == "p3NJ"){
						player3XSpeed = Convert.ToInt16(wiiCommands[i+1]);
					}else if(wiiCommands[i] == "p3IRX"){
						player3AimX = Convert.ToInt16(wiiCommands[i+1]);
					}else if(wiiCommands[i] == "p3IRY"){
						player3AimY = Convert.ToInt16(wiiCommands[i+1]);
					}else 
					
					if(wiiCommands[i] == "p4A"){
						player4Draw=true;
					}else if(wiiCommands[i] == "p4B"){
						player4Shoot=true;
					}else if(wiiCommands[i] == "p4Z"){
						player4Jump=true;
					}else if(wiiCommands[i] == "p4NJ"){
						player4XSpeed = Convert.ToInt16(wiiCommands[i+1]);
					}else if(wiiCommands[i] == "p4IRX"){
						player4AimX = Convert.ToInt16(wiiCommands[i+1]);
					}else if(wiiCommands[i] == "p4IRY"){
						player4AimY = Convert.ToInt16(wiiCommands[i+1]);
					}
				}
			
		}
		
		public int getXSpeed(int playerNumber){
			
			Debug.Log("get playernumber "+playerNumber);
		
				if(playerNumber == 1){
					return player1XSpeed;
				}else if(playerNumber == 2){
					Debug.Log("get = "+player2XSpeed);
					return player2XSpeed;
				}else if(playerNumber == 3){
					return player3XSpeed;
				}else if(playerNumber == 4){
					return player4XSpeed;
				}
				return 0;
		}
		
		public bool getShoot(int playerNumber){
			
			if(playerNumber == 1){
				return player1Shoot;
			}else if(playerNumber == 2){
				return player2Shoot;
			}else if(playerNumber == 3){
				return player3Shoot;
			}else if(playerNumber == 4){
				return player4Shoot;
			}
			return false;
		}
		
		public bool getDraw(int playerNumber){
			
			if(playerNumber == 1){
				return player1Draw;
			}else if(playerNumber == 2){
				return player2Draw;
			}else if(playerNumber == 3){
				return player3Draw;
			}else if(playerNumber == 4){
				return player4Draw;
			}
			return false;
		}
		
		public bool getJump(int playerNumber){
			
			if(playerNumber == 1){
				return player1Jump;
			}else if(playerNumber == 2){
				return player2Jump;
			}else if(playerNumber == 3){
				return player3Jump;
			}else if(playerNumber == 4){
				return player4Jump;
			}
			return false;
		}
		
		public int getAimX(int playerNumber){
			if(playerNumber == 1){
			return player1AimX;
			}else if(playerNumber == 2){
			return player2AimX;
			}else if(playerNumber == 3){
			return player3AimX;
			}else if(playerNumber == 4){
			return player4AimX;
			}
			return 0;
		}
		
		public int getAimY(int playerNumber){
			if(playerNumber == 1){
			return player1AimY;
			}else if(playerNumber == 2){
			return player2AimY;
			}else if(playerNumber == 3){
			return player3AimY;
			}else if(playerNumber == 4){
			return player4AimY;
			}else
			return 0;
		}
}
