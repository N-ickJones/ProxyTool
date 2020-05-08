### Ubuntu 18

#### Hostname
	hostnamectl set-hostname desired_name

#### Run Updates
	apt-get update
	apt-get upgrade

#### Install An Editor
	apt-get install -y vim
	
#### Setup SSH
	vim /etc/ssh/sshd_config
	
##### Basic sshd_config
	HostKey /etc/ssh/ssh_host_rsa_key			# Can disable Keys not used
	HostKey /etc/ssh/ssh_host_ecdsa_key
	HostKey /etc/ssh/ssh_host_ed25519_key
	SyslogFacility AUTHPRIV
	PermitRootLogin without-password			# without-password to disable root login via password
	PubkeyAuthentication yes
	AuthorizedKeysFile .ssh/authorized_keys
	PasswordAuthentication no					# Warning: Only change to yes after key authentication is tested
	ChallengeResponseAuthentication no
	GSSAPIAuthentication yes
	GSSAPICleanupCredentials no
	UsePAM yes
	X11Forwarding yes
	PrintMotd no
	AcceptEnv LANG LC_*
	Subsystem sftp  /usr/libexec/openssh/sftp-server
	
##### Update SSHD
	systemctl daemon-reload
	systemctl restart sshd
	
##### Add Root SSH Key
	mkdir ~/.ssh
	chmod 700 ~/.ssh
	vim ~/.ssh/authorized_keys   # paste public key
	chmod 600 ~/.ssh/authorized_keys
	# Check to make sure key authication is working & update PasswordAuthentication
	
	
### Add An Additional Disk
    fdisk -l
    fdisk /dev/sdb		# Note: replace sdb with your disk and in following steps
        n		# n for new partition
		p 		# for primary
		enter	# default (1) partition number
		enter	# default (2048) first sector
		enter	# default (...) last sector (full hdd)
        w 		# write changes

    mkfs.ext4 /dev/sdb1
	mkdir -p /mount/sdb1
    mount -t ext4 /dev/sdb1 /mount/sdb1

	blkid				# Copy the UUID of sdb1
    vim /etc/fstab     	# Permentant Mount Disk With Following
	
	_# Example Parameters
	UUID="0b8baad3-b208-4d43-82f6-9fa9a80a905c" 
	TYPE="ext4" 
	PARTUUID="00012849-01"
	
	_# fstab file
	UUID=0b8baad3-b208-4d43-82f6-9fa9a80a905c /mount/sdb1 ext4 0 2
	
#### Install Docker
	apt install -y apt-transport-https ca-certificates curl gnupg-agent software-properties-common
	curl -fsSL https://download.docker.com/linux/ubuntu/gpg | sudo apt-key add -
	apt-key fingerprint 0EBFCD88
	add-apt-repository "deb [arch=amd64] https://download.docker.com/linux/ubuntu $(lsb_release -cs) stable"
	apt install -y docker-ce docker-ce-cli containerd.io docker-compose
	
#### Update Docker Default directory if neccessary
	mkdir /mount/sdb1/docker
    vim /etc/docker/daemon.json
		{
			"data-root": "/mount/sdb1/docker"
		}
    systemctl daemon-reload
    systemctl enable docker
	systemctl start docker
	
#### Setup Git
	ssh-keygen -f ~/.ssh/gitKey -t ecdsa -b 521
	cat ~/.ssh/gitKey.public
	eval "$(ssh-agent -s)"
	ssh-add ~/.ssh/gitKey
	_# Add Key github.com > Settings > SSH and GPG keys
	git config --global user.email "email_here"
	git config --global user.name "username_here"
    
	
#### Install Nginx & Cerbot
	add-apt-repository ppa:certbot/certbot
	apt update
	apt install -y nginx certbot python3-certbot-nginx
	certbot certonly --nginx
	certbot renew --dry-run
	
	/etc/letsencrypt/live/myvenv.com/fullchain.pem
	/etc/letsencrypt/live/myvenv.com/privkey.pem
	
	certbot certonly --cert-name myvenv.com
		_# press (1) then paste myvenv.com www.myvenv.com react.myvenv.com razor.myvenv.com django.myvenv.com flask.myvenv.com ruby.myvenv.com spring.myvenv.com laravel.myvenv.com phoenix.myvenv.com express.myvenv.com research.myvenv.com
		_# alternatively (have not tested) -d {above}
	
	_# update ProxyTool/nginx/conf.d/ssl/ssl.config
	
	_# To Refresh Certificate
	certbot renew
	
	_# To Delete a Certificate
	certbot delete --cert-name myvenv.com
	
	mkdir /etc/nginx/cache
	
	vim nginx.conf
		_# changes these settings
		worker_connection 1024;
		types_hash_max_size 4096;
		_# add this after default_type
		proxy_cache_path    /etc/nginx/cache levels=1:2 keys_zone=sammy:30m max_size=10g inactive=60m use_temp_path=off;
	
#### Setup Proxytool
	mkdir -p /opt/www/ProxyTool/
	git clone git@github.com:N-ickJones/ProxyTool.git /opt/www/ProxyTool/
	cp -r /opt/www/ProxyTool/nginx/conf.d/ /etc/nginx/
	systemctl daemon-reload
	systemctl restart nginx
	certbot
		_# press enter for all then 1 for no redirect (already configured)
	cd /opt/www/ProxyTool/
	docker-compose build
	docker-compose up			# add -d at the end of this command after testing
	
	_# docker ps to see status
	_# top
