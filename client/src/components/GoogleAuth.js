import React from 'react';
import { connect } from 'react-redux';
import { signIn, signOut } from '../actions';

class GoogleAuth extends React.Component {
    componentDidMount() {
        window.gapi.load('client:auth2', () => {
            window.gapi.client
                .init({
                    clientId: '792270072289-qrno78ci8045qq93e38edakssugffi3g.apps.googleusercontent.com',
                    scope: 'email'
                })
                .then(() => {
                    this.auth = window.gapi.auth2.getAuthInstance();
                    this.onAuthChange(this.auth.isSignedIn.get());
                    this.auth.isSignedIn.listen(this.onAuthChange);
                });
        });
    }

    //here cause error
    onAuthChange = (isSignedIn) => {
        if (isSignedIn) {
            this.props.signIn(this.auth.currentUser.get().getId());
        } else {
            this.props.signOut();
        }
    }

    onSignInClick = () => {
        // console.log('here onSignInClick');
        this.auth.signIn();
    }

    onSignOutClick = () => {
        // console.log('here onSignOutClick');
        this.auth.signOut();
    }

    renderAuthButton() {
        // console.log('isSignedIn ' + this.props.isSignedIn);

        if (this.props.isSignedIn === null) {
            return null;
        } else if (this.props.isSignedIn) {
            
            return (
                <button className="ui red google button"
                    onClick={this.onSignOutClick}>
                    <i className="google icon" />
                    Sign Out
                </button>

            );
        } else {
            return (
                <button className="ui red google button"
                    onClick={this.onSignInClick}>
                    <i className="google icon" />
                    Sign In
                </button>
            );
        }
    }
    render() {
        return <div>{this.renderAuthButton()}</div>
    }
}

const mapStateToProps = (state) => {
    return { isSignedIn: state.auth.isSignedIn };
}

export default connect(mapStateToProps, { signIn, signOut })(GoogleAuth);