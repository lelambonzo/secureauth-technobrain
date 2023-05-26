# secureauth-technobrain
A bare bones Secure Authentication System written in Java

# Pseudocode:
## Forms
### Signup Entities
- ** Email **: user to enter a valid email address
- ** Password **: user to chooose a password
- ** Validate Password **: user to re-enter password again for confirmation purposes
- ** Submit Button **: clickable button for user to confirm details

### Signin Entities
- ** Email **: user to enter chosen email address
- ** Password **: user to enter chosen password
- ** Submit Button **: clickable button for user to initiate login process

## Logic
### Validation
- ** If Email is blank **: 
  - Error message for required entity
- ** If Password is blank **:
  - Error message for required entity
- ** If Validate Password is blank **:
  - Error message for required entity
- ** If Email is not valid **:
  - Error message for invalid entity
- ** If Validate Password is not valid**:
  - Error message for invalid entity

### Data Storage
- ** User Email **: store in db as is after validation
- ** User Password **: encrypt password after validation then store in db

### Login
- ** Email **: check for a valid email address in the data store
- ** Password **: encrypt password the check the hash again the corresponding email address
- ** Session Token **: setup a new session for the user and log them into the system

### Logout
- ** Session Token **: destroy session token and log user out

### Password reset
- ** Email **: valid date email to ensure it is valid and exists
- ** Reset button **: initiates the password reset logic if email has passed validation
- ** Password Reset Logic **: send password reset link to user email via a separate plugin
