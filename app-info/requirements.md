## Scope (In/Out)
- IN - What will your product do
    - Users will be able to create either a Dungeon Master account with information about their campaign, or a Player account with information about their planned character.
    - Using a simple Swipe Card view, players can find DMs with a campaign to join and DMs can find new players for their campaign.
    - Dungeon Masters will be able to manage and update their parties as well and remove members as they see fit.
    - Players will be able to view their parties' information once they are in one as well and leave if they choose to.
    - Users will be able to upload their own campaign or character artwork to use to represent them.
- OUT - What will your product not do.
    - This app will not be a computer or web application, it is primarily being designed for Android phones.


## Minimum Viable Product & Stretch Goals
#### MVP
- The app will allow a user to register as either a DungeonMaster or a Player, and give them full CRUD operations over both their User and Character information. 
- The main page will utilize a SwipeCard interface to display the appropriate type of other users to the user, if they are a Player it will display DungeonMasters and vice versa. 
- When a DM and a Player have "swiped right" on each other, the player will be added to the Dungeon Master's party and will be able to view the Campaign info as well as info about their party members on a Group Info page. 
- The mobile App will interface with a Web Service API in order to host the SQL database to manage all of the information.
#### Stretch Goals
- We would like to first and foremost make the app look as professional and clean as we possibly can.
- Incorporate Real Time Messaging for Parties within the app, so that the Group Info page includes an real time group chat system.
- Allow users to elect whether they want to find online groups from anywhere in the country or search locally (though meeting in person will of course be discouraged for the remainder of the COVID lockdown/social distancing policies).
## Functional Requirements
- A user can create either a DungeonMaster or Player account, and update any of their profile or character/DM info.
- A user can see all of the opposite types of users from them (i.e. DMs see Players) when viewing the main page, and can make informed decisions about whether they are a good fit to play together.
- A DungeonMaster can remove members from their party, delete their party all together, and update any and all Party/Campaign information.
- A Player can update any of their character information, delete their account, and leave a party if they are in one.
### Data Flow
- When a user opens the app, they will be presented with a login screen or option to register a new account.
- Assuming they are not already a member, they will register and that information will be logged to the SQL database to create a new user.
- Upon first login after registration the user will then be prompted to indicate whether the account they've created will be used for a DungeonMaster or a Player in a game.
- DungeonMasters will subsequently fill out all of their campaign information as well as a short bio about themselves as a DM. Players will create their basic character and provide information about themselves, when a player is created an Active request is also created with all existing DM's who don't have a full party.
- After successfully logging in (and creating a DM/player if they don't have one) the user will be presented with the main page which will be a Swipe Card view showing corresponding User types (DMs for players, vice versa) where they can either swipe or tap the icons to Accept or Reject the current user. If a user is accepted, the request between the two is updated to show that that user has accepted the proposed request, if both users have accepted the Player is added to the DM's party.
- The Profile view page will allow players to view any of their own information, for players this is all their character info, and for DM's this is their overall bio and information.
- The Party view page will allow DungeonMasters and Players to view all information relevant to their party. The DungeonMaster can remove players from their group and update campaign information.
## Non-Functional Requirements
#### Security
- The app will utilize Identity Framework to manage all user information, this will allow for security in accounts and ease of use. The app will also have authorizations in place to ensure that each user can only view/have access to the information they are allowed. The web API will be secured such that it's information is only accessible via users on the actual Mobile Application to ensure data integrity.
#### Ease of use
- The app will have a straightforward layout that will immediately be recognizable to almost anyone who has used or seen many modern dating apps such as Tinder. The swipe card view allows for easy navigable interactions between users and will encourage user's to make informed decisions about the parties they join/build. Finally, being a mobile app developed on Xamarin it should be available/usable by most individuals, though whether or not it will end up on any app stores is yet to be decided.

